using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Identity.BLL.OperationResult;

public interface IOperationResult : IConvertToActionResult
{
    IActionResult Convert();
}
