using Core.Models;

namespace Core.Repositories.AppRoleRepository;

public interface IAppRoleRepository
{
    public Task<AppRole> CreateAppRoleAsync(AppRole Role);
    public Task<AppRole> GetAppRoleAsync(Guid Id);
    public Task<AppRole> GetAppRoleAsync(string Name);
    public Task<IQueryable<AppRole>> GetAllAppRoleAsync();
    public Task DeleteAppRoleAsync(AppRole Role);
}
