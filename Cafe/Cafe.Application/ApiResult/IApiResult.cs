using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Cafe.Application.ApiResult;

public interface IApiResult : IConvertToActionResult
{
    public IActionResult Convert();
}
