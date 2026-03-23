using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Api.Attributes;

/// <summary>
/// Атрибут для добавления заголовка с временем ответа
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ResponseTimeHeaderAttribute : Attribute, IAsyncActionFilter, IAsyncResultFilter
{
    private readonly Stopwatch _stopwatch = new();
    private const string HeaderName = "X-Response-Time-Ms";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _stopwatch.Start();
        await next();
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        context.HttpContext.Response.OnStarting(() =>
        {
            _stopwatch.Stop();

            if (!context.HttpContext.Response.Headers.ContainsKey(HeaderName))
            {
                context.HttpContext.Response.Headers.Append(HeaderName, _stopwatch.ElapsedMilliseconds.ToString());
            }

            return Task.CompletedTask;
        });

        await next();
    }
}