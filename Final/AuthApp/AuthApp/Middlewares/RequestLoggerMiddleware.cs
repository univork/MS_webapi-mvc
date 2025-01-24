namespace AuthApp.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggerMiddleware> _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine(context.Request.Path);
            Console.WriteLine(context.Response.StatusCode);
            _logger.Log(LogLevel.Information, $"REQUEST - {DateTime.Now} - {context.Request.Method}: {context.Request.Path}");
            _logger.Log(LogLevel.Information, $"RESPONSE - {DateTime.Now} - {context.Response.StatusCode}: {context.Response.ContentType}");
            await _next(context);
        }
    }
}
