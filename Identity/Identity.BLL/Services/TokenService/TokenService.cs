using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.BLL.DTO;
using Identity.DAL.Models;
using Identity.DAL.Repositories.AppUserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace Identity.BLL.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly JWTConfig _config;

    public TokenService(IOptions<JWTConfig> config, UserManager<AppUser> userManager)
    {
        _config = config.Value;
        _userManager = userManager;
    }

    public async Task<string> GenerateJWTAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Sid, user.Id.ToString()) };
        claims.AddRange((await _userManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r)));
 
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            notBefore: DateTime.UtcNow,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_config.Key)), 
                    SecurityAlgorithms.HmacSha256)
                );
        
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
