using Identity.DAL.Context;
using Identity.DAL.Models;
using Identity.DAL.Repositories.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Identity.DAL.Repositories.AppUserRepository;

public class AppUserRepository : IAppUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppIdentityContext _context;

    public AppUserRepository(UserManager<AppUser> userManager, AppIdentityContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<AppUser> GetAppUserAsync(Guid id, CancellationToken cancellationToken) 
    { 
        return _context.Users.AsNoTracking().SingleOrDefault(u => u.Id == id);    
    }

    public async Task<AppUser> GetAppUserAsync(string email, CancellationToken cancellationToken) 
    { 
        return _context.Users.AsNoTracking().SingleOrDefault(u => u.NormalizedEmail == email);    
    }

    public async Task<IList<AppUser>> GetAllAppUserAsync(int page, int count, CancellationToken cancellationToken) 
    { 
        return await _context.Users.AsNoTracking().Pagination(page, count).ToListAsync();
    }
}
