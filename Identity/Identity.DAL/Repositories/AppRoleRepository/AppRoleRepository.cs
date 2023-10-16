using Identity.DAL.Context;
using Identity.DAL.Models;
using Identity.DAL.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Identity.DAL.Repositories.AppRoleRepository;

public class AppRoleRepository : IAppRoleRepository
{
    private readonly AppIdentityContext _context;

    public AppRoleRepository(AppIdentityContext context)
    {
        _context = context;
    }

    public async Task<IList<AppRole>> GetAllAppRoleAsync(int page, int count, CancellationToken cancellationToken) 
    { 
        return await _context.Roles.AsNoTracking().Pagination(count, page).ToListAsync();    
    }
}
