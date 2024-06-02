using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleWare(ILogger<RequestTimeLoggingMiddleWare> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        
        await next.Invoke(context);

        stopWatch.Stop();

        var milliseconds = stopWatch.ElapsedMilliseconds;
        var path = context.Request.Path;
        var method = context.Request.Method;

        if (milliseconds / 1000 >= 5)
        {
            logger.LogInformation($"Request [{method}] at {path} took {milliseconds} ms");
        }   
    }
}
