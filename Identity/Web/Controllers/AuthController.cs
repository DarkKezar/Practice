using System.Net;
using System.Security.Claims;
using Core.DTO;
using Core.Models;
using Infr.CustomResult;
using Infr.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IConfiguration configuration;
    private readonly UserManager<AppUser> userManager;

    public AuthController(IAuthService authService, IConfiguration configuration, UserManager<AppUser> userManager){
        this.authService = authService;
        this.configuration = configuration;
        this.userManager = userManager;
    }

    [HttpPost]
    public async Task<ApiResult> AuthAsync(AuthModel model){
        HttpStatusCode httpStatusCode = HttpStatusCode.Created;
        string message = "Success";
        

        try{
            AppUser user = await authService.AuthAccountAsync(model);

            if(user != null){
                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Sid, user.Id.ToString()) };
                claims.AddRange((await userManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r)));
 
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("Jwt:Issuer"),
                    audience: configuration.GetValue<string>("Jwt:Audience"),
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:Key"))), 
                            SecurityAlgorithms.HmacSha256)
                        );
                        
                return new ApiResult(message, httpStatusCode, new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            }else{
                message = "Not valid email or password";
                httpStatusCode = HttpStatusCode.BadRequest;
                return new ApiResult(message, httpStatusCode);
            }
        }catch(Exception e){
            message = e.Message;
            httpStatusCode = (HttpStatusCode)500;
            return new ApiResult(message, httpStatusCode);
        }
    }
}
