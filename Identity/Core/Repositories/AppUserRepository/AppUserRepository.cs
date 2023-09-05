using Core.Context;
using Core.DTO;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Repositories.AppUserRepository;

public class AppUserRepository : IAppUserRepository
{

    private readonly UserManager<AppUser> userManager;
    private readonly AppIdentityContext context;

    public AppUserRepository(UserManager<AppUser> userManager, AppIdentityContext context){
        this.userManager = userManager;
        this.context = context;
    }

    public async Task<AppUser> CreateAppUserAsync(AppUser User, string Password) { 
        var result = await  userManager.CreateAsync(User, Password);
        if(result.Succeeded)
            return User;
        else{
            foreach(var e in result.Errors)
                Console.WriteLine(e.Description);
            return null;
        } 
    }
    public async Task<AppUser> UpdateAppUserAsync(AppUser User) { 
        await userManager.UpdateAsync(User);
        return User;
    }

    public async Task<AppUser> UpdatePasswordAsync(AppUser User, string OldPassword, string NewPassword){
        await userManager.ResetPasswordAsync(User, OldPassword, NewPassword);
        return User;
    }
    public async Task<AppUser> GetAppUserAsync(Guid Id) { 
        return context.Users.SingleOrDefault(u => u.Id == Id);    
    }

    public async Task<AppUser> GetAppUserAsync(string Email) { 
        return context.Users.SingleOrDefault(u => u.NormalizedEmail == Email);    
    }
    public async Task<IQueryable<AppUser>> GetAllAppUserAsync() { 
        return context.Users;
    }
    public async Task DeleteAppUserAsync(AppUser User) { 
        User.IsDeleted = true;
        await userManager.UpdateAsync(User);    
    }
    public async Task<AppUser> AuthAppUserAsync(AuthModel Model) { 
        var User = context.Users.SingleOrDefault(u => u.NormalizedEmail.Equals(Model.Email.ToUpper()));
        Console.WriteLine(User);
        Console.WriteLine(1);
        if(User != null)
            if((await userManager.CheckPasswordAsync(User, Model.Password)) == true)
                return User;
            else
                return null;
        else
            return null;
        
    }

    public async Task<AppUser> AddAppRoleAsync(AppUser User, AppRole Role) { 
        await userManager.AddToRoleAsync(User, Role.Name);
        return User;
    }
    public async Task<AppUser> AddAppRoleAsync(AppUser User, string Role) { 
        await userManager.AddToRoleAsync(User, Role);
        return User;
    }
    public async Task<AppUser> RemoveAppRoleAsync(AppUser User, AppRole Role) { 
        await userManager.RemoveFromRoleAsync(User, Role.Name);
        return User;
    }
    public async Task<AppUser> RemoveAppRoleAsync(AppUser User, string Role) { 
        await userManager.RemoveFromRoleAsync(User, Role);
        return User;
    }
}
