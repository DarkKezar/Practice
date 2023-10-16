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
    public async Task<IActionResult> GetRolesAsync(int page = 1, int count = 10, CancellationToken cancellationToken = default)
    {
        var result = await _appRoleService.GetRolesAsync(page, count, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetRoleAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = await _appRoleService.GetRoleAsync(name, cancellationToken);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = await _appRoleService.CreateRoleAsync(name, cancellationToken);

        return result.Convert();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = await _appRoleService.DeleteRoleAsync(name, cancellationToken);
        
        return result.Convert();
    }
}
