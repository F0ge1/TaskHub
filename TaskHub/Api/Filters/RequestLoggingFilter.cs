using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Api.Filters;

/// <summary>
/// Фильтр для логирования запросов
/// </summary>
public class RequestLoggingFilter : IActionFilter
{
    private readonly ILogger<RequestLoggingFilter> _logger;
    private Stopwatch _stopwatch;

    public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();

        var httpMethod = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path;

        _logger.LogInformation("Начало выполнения запроса: {Method} {Path}", httpMethod, path);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();

        var httpMethod = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path;
        var statusCode = context.HttpContext.Response.StatusCode;
        var elapsedMs = _stopwatch.ElapsedMilliseconds;

        _logger.LogInformation("Завершение запроса: {Method} {Path} -> Статус: {StatusCode}, Время: {ElapsedMs} мс",
            httpMethod, path, statusCode, elapsedMs);
    }
}