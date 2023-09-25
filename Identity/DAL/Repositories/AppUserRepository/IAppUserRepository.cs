using DAL.Models;

namespace DAL.Repositories.AppUserRepository;

public interface IAppUserRepository
{
    public Task<AppUser> CreateAppUserAsync(AppUser user, string password);
    public Task<AppUser> UpdateAppUserAsync(AppUser user);
    public Task<AppUser> UpdatePasswordAsync(AppUser user, string oldPassword, string newPassword);
    public Task<AppUser> GetAppUserAsync(Guid id);
    public Task<AppUser> GetAppUserAsync(string email);
    public Task<IQueryable<AppUser>> GetAllAppUserAsync();
    public Task DeleteAppUserAsync(AppUser user);
    public Task<AppUser> AuthAppUserAsync(string email, string password);
    public Task<AppUser> AddAppRoleAsync(AppUser user, AppRole role);
    public Task<AppUser> AddAppRoleAsync(AppUser user, string role);
    public Task<AppUser> RemoveAppRoleAsync(AppUser user, AppRole role);
    public Task<AppUser> RemoveAppRoleAsync(AppUser user, string role);

}
