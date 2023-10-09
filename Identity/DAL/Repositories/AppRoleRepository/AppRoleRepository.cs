using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.AppRoleRepository;

public class AppRoleRepository : IAppRoleRepository
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly AppIdentityContext _context;

    public AppRoleRepository(RoleManager<AppRole> roleManager, AppIdentityContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<AppRole> CreateAppRoleAsync(AppRole role) 
    { 
        await _roleManager.CreateAsync(role);
        
        return role;
    }
    
    public async Task<AppRole> GetAppRoleAsync(Guid id) 
    { 
        return await _roleManager.FindByIdAsync(id.ToString());
    }

    public async Task<AppRole> GetAppRoleAsync(string name) 
    { 
        return await _roleManager.FindByNameAsync(name);
    }

    public async Task<IQueryable<AppRole>> GetAllAppRoleAsync() 
    { 
        return _context.Roles;    
    }

    public async Task DeleteAppRoleAsync(AppRole role) 
    { 
        await _roleManager.DeleteAsync(role);
    }

}
