using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Infr.Services.AppUserService;
using Infr.CustomResult;
using Core.DTO;
using Core.DTO.UpdateTO;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAppUserService appUserService;

    public UserController(IAppUserService appUserService){
        this.appUserService = appUserService;
    }

    [HttpGet]
    [Route("{email}")]
    public async Task<ApiResult> GetUserAsync(string email){
        return await appUserService.ReadAppUserAsync(email);
    }

    [HttpGet]
    public async Task<ApiResult> GetUsersAsync(int page = 1, int count = 10){
        return await appUserService.ReadAllAppUserAsync(page, count);
    }

    [HttpPost]
    public async Task<ApiResult> CreateUserAsync(SignUpModel model){
        return await appUserService.CreateAppUserAsync(model);
    }

    [HttpPatch]
    [Route("password")]
    public async Task<ApiResult> ResetPasswordAsync(PasswordUpdateModel model){
        return await appUserService.UpdateAppUserAsync(model);
    }

    [HttpPatch]
    [Route("user-data")]
    public async Task<ApiResult> PatchUserDataAsync(AppUserUpdateModel model){
        return await appUserService.UpdateAppUserAsync(model);
    }
}
