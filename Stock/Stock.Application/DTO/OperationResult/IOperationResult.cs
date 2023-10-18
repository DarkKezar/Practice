using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Stock.Application.DTO.OperationResult;

public interface IOperationResult : IConvertToActionResult
{
    IActionResult Convert();
}