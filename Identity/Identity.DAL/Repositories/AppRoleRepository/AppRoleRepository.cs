using Identity.DAL.Context;
using Identity.DAL.Models;

namespace Identity.DAL.Repositories.AppRoleRepository;

public class AppRoleRepository : IAppRoleRepository
{
    private readonly AppIdentityContext _context;

    public AppRoleRepository(AppIdentityContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<AppRole>> GetAllAppRoleAsync() 
    { 
        return _context.Roles;    
    }
}
