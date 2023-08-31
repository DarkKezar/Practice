using Infr.CustomResult;
using Infr.Services.AppRoleService;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IAppRoleService appRoleService;

    public RoleController(IAppRoleService appRoleService){
        this.appRoleService = appRoleService;
    }

    [HttpGet]
    public async Task<ApiResult> GetRolesAsync(int page = 1, int count = 10){
        return await appRoleService.ReadRolesAsync(page, count);
    }

    [HttpGet]
    [Route("{name}")]
    public async Task<ApiResult> GetRoleAsync(string name){
        return await appRoleService.ReadRoleAsync(name);
    }

    [HttpPost]
    public async Task<ApiResult> CreateRoleAsync(string name){
        return await appRoleService.CreateRoleAsync(name);
    }

    [HttpDelete]
    public async Task<ApiResult> DeleteRoleAsync(string name){
        return await appRoleService.DeleteRoleAsync(name);
    }
}
