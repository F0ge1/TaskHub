using System.Diagnostics;

namespace Api.Middlewares
{
    /// <summary>
    /// Класс для замера времени обработки запроса
    /// </summary>
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();

                if (!context.Response.Headers.ContainsKey("X-Response-Time-Ms"))
                {
                    context.Response.Headers.Append("X-Response-Time-Ms", stopwatch.ElapsedMilliseconds.ToString());

                    var path = context.Request.Path;
                    var method = context.Request.Method;
                }

                return Task.CompletedTask;
            });

            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                stopwatch.Stop();
                throw;
            }
        }
    }
}
