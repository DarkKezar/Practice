using Identity.DAL.Models;

namespace Identity.DAL.Repositories.AppUserRepository;

public interface IAppUserRepository
{
    public Task<AppUser> GetAppUserAsync(Guid id);
    public Task<AppUser> GetAppUserAsync(string email);
    public Task<IQueryable<AppUser>> GetAllAppUserAsync();
}
