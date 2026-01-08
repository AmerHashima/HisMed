using HIS.Api.Models;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace HIS.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = exception switch
        {
            KeyNotFoundException => ApiResponse.ErrorResult(
                exception.Message, 
                statusCode: (int)HttpStatusCode.NotFound),
            
            InvalidOperationException => ApiResponse.ErrorResult(
                exception.Message, 
                statusCode: (int)HttpStatusCode.BadRequest),
            
            ArgumentException => ApiResponse.ErrorResult(
                exception.Message, 
                statusCode: (int)HttpStatusCode.BadRequest),
            
            UnauthorizedAccessException => ApiResponse.ErrorResult(
                "Unauthorized access", 
                statusCode: (int)HttpStatusCode.Unauthorized),
            
            ReflectionTypeLoadException reflectionEx => ApiResponse.ErrorResult(
                "Application startup error - type loading failed",
                reflectionEx.LoaderExceptions?.Select(ex => ex?.Message ?? "Unknown loader exception").ToList(),
                (int)HttpStatusCode.InternalServerError,
                reflectionEx.InnerException?.Message),
            
            _ => ApiResponse.ErrorResult(
                "An internal server error occurred",
                new List<string> { exception.Message },
                (int)HttpStatusCode.InternalServerError,
                exception.InnerException?.Message)
        };

        response.TraceId = context.TraceIdentifier;
        context.Response.StatusCode = response.StatusCode;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}