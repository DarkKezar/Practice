using Identity.BLL.OperationResult;

namespace Identity.BLL.Services.AppRoleService;

public interface IAppRoleService
{
    Task<IOperationResult> CreateRoleAsync(string name, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetRoleAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetRoleAsync(string name, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetRolesAsync(int page = 1, int count = 10, CancellationToken cancellationToken = default);
    Task<IOperationResult> DeleteRoleAsync(string name, CancellationToken cancellationToken = default);
}
