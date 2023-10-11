using System.Net;
using Identity.DAL.Models;
using Identity.DAL.Repositories.AppRoleRepository;
using Identity.BLL.CustomResult;
using Microsoft.AspNetCore.Identity;

namespace Identity.BLL.Services.AppRoleService;

public class AppRoleService : IAppRoleService
{
    private readonly IAppRoleRepository _appRoleRepository;
    private readonly RoleManager<AppRole> _roleManager;

    public AppRoleService(IAppRoleRepository appRoleRepository, RoleManager<AppRole> roleManager)
    {
        _appRoleRepository = appRoleRepository;
        _roleManager = roleManager;
    }

    public async Task<IApiResult> CreateRoleAsync(string name)
    {
        AppRole role = new AppRole(name);
        await _roleManager.CreateAsync(role);

        return new OperationResult<AppRole>(Messages.Created, HttpStatusCode.Created, role);
    }

    public async Task<IApiResult> DeleteRoleAsync(string name)
    {
        AppRole role = await _roleManager.FindByNameAsync(name);
        if(role == null) throw new Exception(Messages.NotFound);
        await _roleManager.DeleteAsync(role);

        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IApiResult> GetRoleAsync(Guid id)
    {
        AppRole role = await _roleManager.FindByIdAsync(id.ToString());
        if(role == null) throw new Exception(Messages.NotFound);
        
        return new OperationResult<AppRole>(Messages.Success, HttpStatusCode.OK, role);
    }

    public async Task<IApiResult> GetRoleAsync(string name)
    {
        AppRole role = await _roleManager.FindByNameAsync(name);
        if(role == null) throw new Exception(Messages.NotFound);
        
        return new OperationResult<AppRole>(Messages.Success, HttpStatusCode.OK, role);
    }

    public async Task<IApiResult> GetRolesAsync(int page = 1, int count = 10)
    {
        List<AppRole> roles  = (await _appRoleRepository.GetAllAppRoleAsync())
                                .Skip((page - 1) * count).Take(count).ToList();

        return new OperationResult<List<AppRole>>(Messages.Success, HttpStatusCode.OK, roles);
    }
}
