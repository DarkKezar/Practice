using DAL.Models;
using BLL.DTO;
using BLL.CustomResult;

namespace BLL.Services.AuthService;

public interface IAuthService
{
    public Task<IApiResult> AuthAccountAsync(AuthModel model, JWTConfig config);
}
