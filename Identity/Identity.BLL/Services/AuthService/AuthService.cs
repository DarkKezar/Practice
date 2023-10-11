using Identity.DAL.Models;
using Identity.DAL.Repositories.AppUserRepository;
using Identity.BLL.DTO;
using Identity.BLL.CustomResult;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Identity.BLL.Services.TokenService;


namespace Identity.BLL.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;



    public AuthService(IAppUserRepository appUserRepository, UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _appUserRepository = appUserRepository;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<IApiResult> AuthAccountAsync(AuthModel model)
    {    
        AppUser user = await _appUserRepository.GetAppUserAsync(model.Email); 
        if(user != null)
        {
            if((await _userManager.CheckPasswordAsync(user, model.Password)) == true)
            {
                return new OperationResult<string>(Messages.Success, HttpStatusCode.Accepted, await _tokenService.GenerateJWTAsync(user));
            }else
            {
                return new OperationResult<Object>(Messages.InvalidPassword, HttpStatusCode.BadRequest);        
            }
        }else
        {
            return new OperationResult<Object>(Messages.InvalidEmail, HttpStatusCode.BadRequest);
        }            
    }
}
