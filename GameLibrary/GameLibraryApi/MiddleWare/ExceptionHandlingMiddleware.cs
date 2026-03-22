namespace GameLibraryAPI.MiddleWare;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // This method is called for every HTTP request
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the error so we can see it in the console
            _logger.LogError(ex, "An unhandled exception occurred");

            // Return a standardized JSON error response instead of a stack trace
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                status = 500,
                message = "An unexpected error occurred",
                detail = ex.Message
            });
        }
    }
}