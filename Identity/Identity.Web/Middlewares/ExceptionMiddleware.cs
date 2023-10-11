using System.Net;

namespace Identity.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ILoggerManager _logger;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorDetails() { StatusCode = context.Response.StatusCode, Message = e.Message }.ToString());
    }
}

internal class ErrorDetails
{
    public ErrorDetails()
    {
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
}