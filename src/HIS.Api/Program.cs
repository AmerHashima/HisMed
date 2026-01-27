using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using HIS.Api.Middleware;
using HIS.Application;
using HIS.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // This should work with correct package version

var builder = WebApplication.CreateBuilder(args);

try
{
    // --------------------------
    // Add services to the container
    // --------------------------
    builder.Services.AddControllers();

    // Add Application & Infrastructure layers
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    // --------------------------
    // Configure FluentValidation
    // --------------------------
    builder.Services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

    // --------------------------
    // Configure CORS to allow any region
    // --------------------------
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()      // Allow requests from any origin/region
                  .AllowAnyMethod()      // Allow any HTTP method
                  .AllowAnyHeader();     // Allow any headers
        });
    });

    // --------------------------
    // Configure JWT Authentication
    // --------------------------
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"];

    if (string.IsNullOrEmpty(secretKey))
    {
        throw new InvalidOperationException("JWT SecretKey is not configured.");
    }

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };

        // Add events for better error handling
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Append("Token-Expired", "true");
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new
                {
                    success = false,
                    message = "You are not authorized to access this resource",
                    data = (object?)null
                });
                return context.Response.WriteAsync(result);
            }
        };
    });

    builder.Services.AddAuthorization();

    // --------------------------
    // Configure Swagger with JWT and Global Authorization
    // --------------------------
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Hospital Information System API",
            Version = "v1",
            Description = "HIS API with JWT Authentication - Use the Authorize button to add your Bearer token",
            Contact = new OpenApiContact
            {
                Name = "HIS Development Team",
                Email = "support@his.com",
                Url = new Uri("https://his.com/support")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        });

        // Add JWT Authentication to Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        // Global security requirement - applies to ALL endpoints
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });

        // Include XML comments for better documentation
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }

        // Enable annotations for better Swagger documentation
        c.EnableAnnotations();
    });

    // --------------------------
    // Add Health Checks
    // --------------------------
    builder.Services.AddHealthChecks()
        .AddDbContextCheck<HIS.Infrastructure.Persistence.HISDbContext>("database");

    // --------------------------
    // Build app
    // --------------------------
    var app = builder.Build();

    // --------------------------
    // Configure middleware pipeline
    // --------------------------
    
    // Global exception handling
    app.UseMiddleware<GlobalExceptionMiddleware>();

    // Enable Swagger for all environments
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HIS API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "HIS API Documentation";
        c.DisplayRequestDuration();
        c.EnableTryItOutByDefault();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        c.DefaultModelsExpandDepth(-1);
        c.EnableFilter();
        c.EnableDeepLinking();
        c.ConfigObject.AdditionalItems["persistAuthorization"] = true;
    });

    // Health check endpoints
    app.MapHealthChecks("/api/health");
    app.MapHealthChecks("/api/health/ready");
    app.MapHealthChecks("/health");

    // Redirect root to Swagger
    app.MapGet("/", () => Results.Redirect("/swagger"));

    // ⭐ CORS must be before Authentication and Authorization
    app.UseCors("AllowAll");

    app.UseHttpsRedirection();

    // Authentication must come before Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // --------------------------
    // Log startup information
    // --------------------------
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("=================================================");
    logger.LogInformation("HIS API is starting up...");
    logger.LogInformation("Environment: {Environment}", app.Environment.EnvironmentName);
    logger.LogInformation("CORS Policy: AllowAll - Accepting requests from any origin");
    logger.LogInformation("Swagger UI: /swagger");
    logger.LogInformation("Health Check: /api/health");
    logger.LogInformation("=================================================");

    app.Run();
}
catch (ReflectionTypeLoadException ex)
{
    Console.WriteLine("=================================================");
    Console.WriteLine("ReflectionTypeLoadException occurred:");
    Console.WriteLine($"Message: {ex.Message}");

    if (ex.LoaderExceptions != null)
    {
        Console.WriteLine("Loader Exceptions:");
        foreach (var loaderEx in ex.LoaderExceptions)
        {
            if (loaderEx != null)
            {
                Console.WriteLine($"  - {loaderEx.Message}");
                if (loaderEx.InnerException != null)
                {
                    Console.WriteLine($"    Inner: {loaderEx.InnerException.Message}");
                }
            }
        }
    }
    Console.WriteLine("=================================================");
    throw;
}
catch (Exception ex)
{
    Console.WriteLine("=================================================");
    Console.WriteLine($"Startup error: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
    }
    Console.WriteLine("=================================================");
    throw;
}

public partial class Program { }
