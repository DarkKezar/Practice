using Identity.DAL.Models;

namespace Identity.BLL.Services.TokenService;

public interface ITokenService
{
    Task<string> GenerateJWTAsync(AppUser user, CancellationToken cancellationToken = default);
}
