using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Stock.Application.DTO.ApiResult;

public interface IApiResult : IConvertToActionResult
{
    public IActionResult Convert();
}
