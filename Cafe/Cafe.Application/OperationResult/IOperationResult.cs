using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Cafe.Application.OperationResult;

public interface IOperationResult : IConvertToActionResult
{
    IActionResult Convert();
}
