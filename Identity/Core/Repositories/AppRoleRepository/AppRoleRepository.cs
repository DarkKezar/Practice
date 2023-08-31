using Core.Context;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Repositories.AppRoleRepository;

public class AppRoleRepository : IAppRoleRepository
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly AppIdentityContext context;

    public AppRoleRepository(RoleManager<AppRole> roleManager, AppIdentityContext context){
        this.roleManager = roleManager;
        this.context = context;
    }

    public async Task<AppRole> CreateAppRoleAsync(AppRole Role) { 
        await roleManager.CreateAsync(Role);
        return Role;
    }
    public async Task<AppRole> GetAppRoleAsync(Guid Id) { 
        return await roleManager.FindByIdAsync(Id.ToString());
    }
    public async Task<AppRole> GetAppRoleAsync(string Name) { 
        return await roleManager.FindByNameAsync(Name);
    }
    public async Task<IQueryable<AppRole>> GetAllAppRoleAsync() { 
        return context.Roles;    
    }
    public async Task DeleteAppRoleAsync(AppRole Role) { 
        await roleManager.DeleteAsync(Role);
    }

}
