using Identity.BLL.DTO;
using Identity.BLL.OperationResult;

namespace Identity.BLL.Services.AppUserService;

public interface IAppUserService
{
    Task<IOperationResult> CreateAppUserAsync(SignUpModel model, CancellationToken cancellationToken = default);
    Task<IOperationResult> UpdateAppUserAsync(Guid userId, AppUserUpdateModel model, CancellationToken cancellationToken = default);
    Task<IOperationResult> UpdateAppUserPasswrodAsync(Guid userId, PasswordUpdateModel model, CancellationToken cancellationToken = default);
    Task<IOperationResult> DeleteAppUserAsync(string email, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetAppUserAsync(string email, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetAllAppUserAsync(int page = 1, int count = 10, CancellationToken cancellationToken = default);
    Task<IOperationResult> AddToRoleAsync(string email, string role, CancellationToken cancellationToken = default);
    Task<IOperationResult> RemoveFromRoleAsync(string email, string role, CancellationToken cancellationToken = default);
}
