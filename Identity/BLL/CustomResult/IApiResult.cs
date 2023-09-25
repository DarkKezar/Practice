using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BLL.CustomResult;

public enum StatusCode : HttpStatusCode{
    Accepted = HttpStatusCode.Accepted,
    Created = HttpStatusCode.Created,
    ServerError = (int)500,
    DataError = (int)400
}

public enum Messages : string{
    Accepted = "Success",
    Created = "Created",
}
public interface IApiResult : IConvertToActionResult
{
    
    public IActionResult Convert();
}
