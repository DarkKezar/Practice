using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Cafe.Application.ApiResult;

public class OperationResult<T> : IApiResult
{
    public readonly string Message;
    public readonly HttpStatusCode HttpStatusCode;
    public readonly T? ObjectResult;

    public OperationResult(string message, HttpStatusCode httpStatusCode, T? objectResult = default(T))
    {
        Message = message;
        HttpStatusCode = httpStatusCode;
        ObjectResult = objectResult;
    }

    public IActionResult Convert()
    {
        Object Result = new {
            Message = Message,
            Value = ObjectResult
        };


        return new ObjectResult(Result){
            StatusCode = (int)HttpStatusCode
        };
    }
}
