namespace mentorship_program_tool.Middleware
{
    public sealed class LogMiddleware
    {
        private readonly ILogger<LogMiddleware> _logger;
        private readonly RequestDelegate _next;
        public LogMiddleware(ILoggerFactory logger, RequestDelegate next)
        {
            _logger = logger?.CreateLogger<LogMiddleware>() ?? throw new ArgumentNullException(nameof(logger));
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation(context.Request.Method + " " + context.Request.Path + " " + context.Response.StatusCode);
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "an exception occured");
            }
        }
    }
}
