using BLL.CustomResult;
using BLL.Services.AppRoleService;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IAppRoleService _appRoleService;

    public RoleController(IAppRoleService appRoleService)
    {
        _appRoleService = appRoleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRolesAsync(int page = 1, int count = 10)
    {
        return (await _appRoleService.ReadRolesAsync(page, count)).Convert();
    }

    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetRoleAsync(string name)
    {
        return (await _appRoleService.ReadRoleAsync(name)).Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(string name)
    {
        return (await _appRoleService.CreateRoleAsync(name)).Convert();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync(string name)
    {
        return (await _appRoleService.DeleteRoleAsync(name)).Convert();
    }
}
