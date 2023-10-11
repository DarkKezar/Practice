using Identity.BLL.DTO;
using Identity.BLL.CustomResult;

namespace Identity.BLL.Services.AuthService;

public interface IAuthService
{
    public Task<IApiResult> AuthAccountAsync(AuthModel model);
}
