using Identity.DAL.Models;

namespace Identity.BLL.Services.TokenService;

public interface ITokenService
{
    public Task<string> GenerateJWTAsync(AppUser user);
}
