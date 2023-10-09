using DAL.Models;
using DAL.Repositories.AppUserRepository;
using BLL.DTO;
using BLL.CustomResult;
using System.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace BLL.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IAppUserRepository _appUserRepository;
    //private readonly UserManager<AppUser> _userManager;


    //public AuthService(IAppUserRepository appUserRepository, UserManager<AppUser> userManager)
    public AuthService(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
        //_userManager = userManager;
    }

    public async Task<IApiResult> AuthAccountAsync(AuthModel model, JWTConfig config)
    {

        HttpStatusCode httpStatusCode = HttpStatusCode.Created;
        string message = "Success";
        

        try
        {
            AppUser user = await _appUserRepository.AuthAppUserAsync(model.Email, model.Password);

            if(user != null)
            {
                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Sid, user.Id.ToString()) };
                //claims.AddRange((await _userManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r)));
 
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    //issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                    issuer: config.Issuer,
                    //audience: _configuration.GetValue<string>("Jwt:Audience"),
                    audience: config.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            //Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"))), 
                            Encoding.ASCII.GetBytes(config.Key)), 
                            SecurityAlgorithms.HmacSha256)
                        );
                        
                return new OperationResult<string>(message, httpStatusCode, new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            }else
            {
                message = "Not valid email or password";
                httpStatusCode = HttpStatusCode.BadRequest;

                return new OperationResult<Object>(message, httpStatusCode);
            }
        }catch(Exception e)
        {
            message = e.Message;
            httpStatusCode = (HttpStatusCode)500;
            
            return new OperationResult<Object>(message, httpStatusCode);
        }
    }
}
