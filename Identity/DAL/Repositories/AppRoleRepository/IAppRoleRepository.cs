using DAL.Models;

namespace DAL.Repositories.AppRoleRepository;

public interface IAppRoleRepository
{
    public Task<AppRole> CreateAppRoleAsync(AppRole role);
    public Task<AppRole> GetAppRoleAsync(Guid id);
    public Task<AppRole> GetAppRoleAsync(string name);
    public Task<IQueryable<AppRole>> GetAllAppRoleAsync();
    public Task DeleteAppRoleAsync(AppRole role);
}
