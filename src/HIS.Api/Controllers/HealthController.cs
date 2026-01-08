using HIS.Api.Models;
using HIS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
public class HealthController : BaseApiController
{
    private readonly HISDbContext _context;
    private readonly ILogger<HealthController> _logger;

    public HealthController(HISDbContext context, ILogger<HealthController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Basic health check endpoint
    /// </summary>
    /// <returns>API health status</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<HealthCheckResponse>>> Get()
    {
        try
        {
            var healthCheck = new HealthCheckResponse
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = GetApplicationVersion(),
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                Uptime = GetUptime()
            };

            _logger.LogInformation("Health check performed successfully");
            return SuccessResponse(healthCheck, "API is healthy");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed");
            return ErrorResponse<HealthCheckResponse>("Health check failed", 503);
        }
    }

    /// <summary>
    /// Detailed health check with database connectivity
    /// </summary>
    /// <returns>Detailed health status including database connection</returns>
    [HttpGet("detailed")]
    public async Task<ActionResult<ApiResponse<DetailedHealthCheckResponse>>> GetDetailed()
    {
        var healthCheck = new DetailedHealthCheckResponse
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = GetApplicationVersion(),
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            Uptime = GetUptime(),
            Checks = new List<HealthCheckItem>()
        };

        // Check API
        healthCheck.Checks.Add(new HealthCheckItem
        {
            Name = "API",
            Status = "Healthy",
            Description = "API is responding"
        });

        // Check Database
        try
        {
            await _context.Database.CanConnectAsync();
            var dbVersion = await _context.Database.ExecuteSqlRawAsync("SELECT 1");
            
            healthCheck.Checks.Add(new HealthCheckItem
            {
                Name = "Database",
                Status = "Healthy",
                Description = "Database connection successful"
            });

            _logger.LogInformation("Detailed health check completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database health check failed");
            
            healthCheck.Status = "Unhealthy";
            healthCheck.Checks.Add(new HealthCheckItem
            {
                Name = "Database",
                Status = "Unhealthy",
                Description = $"Database connection failed: {ex.Message}"
            });

            return ErrorResponse<DetailedHealthCheckResponse>("System is unhealthy", 503);
        }

        return SuccessResponse(healthCheck, "System is healthy");
    }

    /// <summary>
    /// Readiness probe for Kubernetes
    /// </summary>
    /// <returns>Readiness status</returns>
    [HttpGet("ready")]
    public async Task<ActionResult<ApiResponse<HealthCheckResponse>>> Ready()
    {
        try
        {
            // Check database connectivity for readiness
            await _context.Database.CanConnectAsync();

            var readinessCheck = new HealthCheckResponse
            {
                Status = "Ready",
                Timestamp = DateTime.UtcNow,
                Version = GetApplicationVersion(),
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                Uptime = GetUptime()
            };

            return SuccessResponse(readinessCheck, "API is ready");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Readiness check failed");
            return ErrorResponse<HealthCheckResponse>("API is not ready", 503);
        }
    }

    /// <summary>
    /// Liveness probe for Kubernetes
    /// </summary>
    /// <returns>Liveness status</returns>
    [HttpGet("live")]
    public ActionResult<ApiResponse<HealthCheckResponse>> Live()
    {
        var livenessCheck = new HealthCheckResponse
        {
            Status = "Alive",
            Timestamp = DateTime.UtcNow,
            Version = GetApplicationVersion(),
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            Uptime = GetUptime()
        };

        return SuccessResponse(livenessCheck, "API is alive");
    }

    private string GetApplicationVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version;
        return version?.ToString() ?? "Unknown";
    }

    private TimeSpan GetUptime()
    {
        using var process = Process.GetCurrentProcess();
        return DateTime.UtcNow - process.StartTime.ToUniversalTime();
    }
}

public class HealthCheckResponse
{
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Version { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public TimeSpan Uptime { get; set; }
}

public class DetailedHealthCheckResponse : HealthCheckResponse
{
    public List<HealthCheckItem> Checks { get; set; } = new();
}

public class HealthCheckItem
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}