using Identity.BLL.Services.AppRoleService;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoleController : Controller
{
    private readonly IAppRoleService _appRoleService;

    public RoleController(IAppRoleService appRoleService)
    {
        _appRoleService = appRoleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRolesAsync(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int count = 10)
    {
        var result = await _appRoleService.GetRolesAsync(page, count, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetRoleAsync(CancellationToken cancellationToken, string name)
    {
        var result = await _appRoleService.GetRoleAsync(name, cancellationToken);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(CancellationToken cancellationToken, string name)
    {
        var result = await _appRoleService.CreateRoleAsync(name, cancellationToken);

        return result.Convert();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync(CancellationToken cancellationToken, string name)
    {
        var result = await _appRoleService.DeleteRoleAsync(name, cancellationToken);
        
        return result.Convert();
    }
}
