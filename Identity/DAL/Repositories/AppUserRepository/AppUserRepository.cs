using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.AppUserRepository;

public class AppUserRepository : IAppUserRepository
{

    private readonly UserManager<AppUser> _userManager;
    private readonly AppIdentityContext _context;

    public AppUserRepository(UserManager<AppUser> userManager, AppIdentityContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<AppUser> CreateAppUserAsync(AppUser user, string password) 
    { 
        var result = await  _userManager.CreateAsync(user, password);
        if(result.Succeeded)

            return user;
        else{
            foreach(var e in result.Errors)
                Console.WriteLine(e.Description);
                //elk

            return null;
        } 
    }
    public async Task<AppUser> UpdateAppUserAsync(AppUser user) 
    { 
        await _userManager.UpdateAsync(user);

        return user;
    }

    public async Task<AppUser> UpdatePasswordAsync(AppUser user, string oldPassword, string newPassword)
    {
        await _userManager.ResetPasswordAsync(user, oldPassword, newPassword);

        return user;
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

    public async Task DeleteAppUserAsync(AppUser user) 
    { 
        user.IsDeleted = true;
        await _userManager.UpdateAsync(user);    
    }

    public async Task<AppUser> AuthAppUserAsync(string email, string password) 
    { 
        var User = _context.Users.SingleOrDefault(u => u.NormalizedEmail.Equals(email.ToUpper()));
        if(User != null)
        {
            if((await _userManager.CheckPasswordAsync(User, password)) == true)
            {
                return User;

            }else
            {
                return null;
            }
        }else
        {
            return null;
        }
    }

    public async Task<AppUser> AddAppRoleAsync(AppUser user, AppRole role) 
    { 
        await _userManager.AddToRoleAsync(user, role.Name);

        return user;
    }

    public async Task<AppUser> AddAppRoleAsync(AppUser user, string role) 
    { 
        await _userManager.AddToRoleAsync(user, role);

        return user;
    }

    public async Task<AppUser> RemoveAppRoleAsync(AppUser user, AppRole role) 
    { 
        await _userManager.RemoveFromRoleAsync(user, role.Name);

        return user;
    }
    public async Task<AppUser> RemoveAppRoleAsync(AppUser user, string role) 
    { 
        await _userManager.RemoveFromRoleAsync(user, role);
        
        return user;
    }
}
