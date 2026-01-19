using FluentValidation.AspNetCore;
using HIS.Api.Middleware;
using HIS.Application;
using HIS.Application.Validators.SystemUser;
using HIS.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    // Configure Swagger with JWT support
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HIS API", Version = "v1" });

        // Add JWT Authentication to Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(document => new OpenApiSecurityRequirement

        {

            [new OpenApiSecuritySchemeReference("foo", document)] = [],

            [new OpenApiSecuritySchemeReference("bar", document)] = []

        });
    });

    // Add JWT Authentication
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

    builder.Services.AddAuthorization();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    // Add FluentValidation
    builder.Services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
      builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()      // Allow requests from any origin/region
                  .AllowAnyMethod()      // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                  .AllowAnyHeader();    // Allow any headers
                  //.WithExposedHeaders("Token-Expired"); // Expose custom headers to clients
        });

        // Alternative: Named policy for specific origins (if needed later)
        //options.AddPolicy("AllowSpecificOrigins", policy =>
        //{
        //    policy.WithOrigins(
        //            "http://localhost:3000",      // React default
        //            "http://localhost:4200",      // Angular default
        //            "http://localhost:5173",      // Vite default
        //            "https://yourdomain.com"      // Production domain
        //          )
        //          .AllowAnyMethod()
        //          .AllowAnyHeader()
        //          .AllowCredentials();  // Allow credentials with specific origins
        //});
    });
  
    var app = builder.Build();

    // Add global exception handling middleware
    app.UseMiddleware<GlobalExceptionMiddleware>();

    // Configure the HTTP request pipeline - Enable Swagger for all environments in containerized deployment
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HIS API v1");
        c.RoutePrefix = "swagger"; // Swagger UI at /swagger
        c.DisplayRequestDuration();
        c.EnableTryItOutByDefault();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });

    // Health check endpoints
    //app.MapHealthChecks("/api/health");
    //app.MapHealthChecks("/api/health/ready");

    // Redirect root to Swagger in containerized environments
    app.MapGet("/", () => Results.Redirect("/swagger"));

    app.UseHttpsRedirection();

    // Add authentication and authorization middleware
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseCors("AllowAll");
    app.Run();
}
catch (ReflectionTypeLoadException ex)
{
    // Log detailed information about the type loading failure
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

    throw; // Re-throw to maintain the original behavior
}
catch (Exception ex)
{
    Console.WriteLine($"Startup error: {ex.Message}");
    throw;
}
public partial class Program { }