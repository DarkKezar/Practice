using Core.DTO;
using Core.Models;

namespace Core.Repositories.AppUserRepository;

public interface IAppUserRepository
{
    public Task<AppUser> CreateAppUserAsync(AppUser User, string Password);
    public Task<AppUser> UpdateAppUserAsync(AppUser User);
    public Task<AppUser> UpdatePasswordAsync(AppUser User, string OldPassword, string NewPassword);
    public Task<AppUser> GetAppUserAsync(Guid Id);
    public Task<AppUser> GetAppUserAsync(string Email);
    public Task<IQueryable<AppUser>> GetAllAppUserAsync();
    public Task DeleteAppUserAsync(AppUser User);
    public Task<AppUser> AuthAppUserAsync(AuthModel Model);
    public Task<AppUser> AddAppRoleAsync(AppUser User, AppRole Role);
    public Task<AppUser> AddAppRoleAsync(AppUser User, string Role);
    public Task<AppUser> RemoveAppRoleAsync(AppUser User, AppRole Role);
    public Task<AppUser> RemoveAppRoleAsync(AppUser User, string Role);

}
