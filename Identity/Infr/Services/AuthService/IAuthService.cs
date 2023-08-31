using Core.DTO;
using Core.Models;

namespace Infr.Services.AuthService;

public interface IAuthService
{
    public Task<AppUser> AuthAccountAsync(AuthModel model);
}
