using Identity.DAL.Models;

namespace Identity.DAL.Repositories.AppRoleRepository;

public interface IAppRoleRepository
{
    public Task<IQueryable<AppRole>> GetAllAppRoleAsync();
}
