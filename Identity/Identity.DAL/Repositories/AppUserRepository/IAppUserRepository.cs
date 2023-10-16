using Identity.DAL.Models;

namespace Identity.DAL.Repositories.AppUserRepository;

public interface IAppUserRepository
{
    Task<AppUser> GetAppUserAsync(Guid id, CancellationToken cancellationToken);
    Task<AppUser> GetAppUserAsync(string email, CancellationToken cancellationToken);
    Task<IList<AppUser>> GetAllAppUserAsync(int page, int count, CancellationToken cancellationToken);
}
