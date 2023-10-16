using Identity.DAL.Models;

namespace Identity.DAL.Repositories.AppRoleRepository;

public interface IAppRoleRepository
{
    Task<IList<AppRole>> GetAllAppRoleAsync(int page, int count, CancellationToken cancellationToken);
}
