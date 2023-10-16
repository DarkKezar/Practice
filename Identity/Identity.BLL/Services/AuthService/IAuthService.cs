using Identity.BLL.DTO;
using Identity.BLL.OperationResult;

namespace Identity.BLL.Services.AuthService;

public interface IAuthService
{
    Task<IOperationResult> AuthAccountAsync(AuthModel model, CancellationToken cancellationToken = default);
}
