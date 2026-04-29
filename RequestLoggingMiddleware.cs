public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        await _next(context);

        watch.Stop();
        Console.WriteLine($"Request [{context.Request.Method}] {context.Request.Path} executed in {watch.ElapsedMilliseconds}ms");
    }
}