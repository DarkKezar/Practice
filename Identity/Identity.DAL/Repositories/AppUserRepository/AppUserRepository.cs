using Identity.DAL.Context;
using Identity.DAL.Models;
using Microsoft.AspNetCore.Identity;

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

    public async Task<AppUser> GetAppUserAsync(Guid id) 
    { 
        return _context.Users.SingleOrDefault(u => u.Id == id);    
    }

    public async Task<AppUser> GetAppUserAsync(string email) 
    { 
        return _context.Users.SingleOrDefault(u => u.NormalizedEmail == email);    
    }

    public async Task<IQueryable<AppUser>> GetAllAppUserAsync() 
    { 
        return _context.Users;
    }
}
