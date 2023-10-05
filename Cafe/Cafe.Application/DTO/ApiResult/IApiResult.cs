using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Cafe.Application.DTO.ApiResult;

public interface IApiResult : IConvertToActionResult
{
    public IActionResult Convert();
}
