using Microsoft.AspNetCore.Mvc;
using BLL.Services.AppUserService;
using BLL.DTO;
using BLL.DTO.UpdateTO;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAppUserService _appUserService;

    public UserController(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    [HttpGet]
    [Route("{email}")]
    public async Task<IActionResult> GetUserAsync(string email)
    {
        return (await _appUserService.GetAppUserAsync(email)).Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync(int page = 1, int count = 10)
    {
        return (await _appUserService.GetAllAppUserAsync(page, count)).Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] SignUpModel model)
    {
        return (await _appUserService.CreateAppUserAsync(model)).Convert();
    }

    [HttpPatch]
    [Route("password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] PasswordUpdateModel model)
    {
        return (await _appUserService.UpdateAppUserAsync(model)).Convert();
    }

    [HttpPatch]
    [Route("user-data")]
    public async Task<IActionResult> PatchUserDataAsync([FromBody] AppUserUpdateModel model)
    {
        return (await _appUserService.UpdateAppUserAsync(model)).Convert();
    }
}
