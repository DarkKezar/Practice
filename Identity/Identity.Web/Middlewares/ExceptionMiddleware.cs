using System.Net;
using System.Text.Json;
using Identity.BLL.DTO;

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
        context.Response.StatusCode = (e is OperationWebException) ? (int)((OperationWebException)e).HttpStatusCode : (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails(context.Response.StatusCode, e.Message)));
    }
}

internal class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ErrorDetails()
    { }

    public ErrorDetails(int statusCode, string message)
    { 
        StatusCode = statusCode;
        Message = message;
    }
}