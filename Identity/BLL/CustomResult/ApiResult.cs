using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BLL.CustomResult;

public class ApiResult : IApiResult
{
    public readonly string Message;
    public readonly HttpStatusCode HttpStatusCode;

    public ApiResult(string message, HttpStatusCode httpStatusCode)
    {
        Message = message;
        HttpStatusCode = httpStatusCode;
    }

    public IActionResult Convert()
    {
        Object Result = new {
            Message = Message,
        };

        
        return new ObjectResult(Result){
            StatusCode = (int)HttpStatusCode
        };
    }
}
