using Microsoft.AspNetCore.Mvc;
using Identity.BLL.Services.AppUserService;
using Identity.BLL.DTO;
using Identity.Web.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IAppUserService _appUserService;

    public UserController(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    [HttpGet]
    [Route("{email}")]
    public async Task<IActionResult> GetUserAsync(CancellationToken cancellationToken, string email)
    {
        var result = await _appUserService.GetAppUserAsync(email, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("{page}")]
    public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken, int page = 1, [FromBody] int count = 10)
    {
        var result = await _appUserService.GetAllAppUserAsync(page, count, cancellationToken);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(CancellationToken cancellationToken, [FromBody] SignUpModel model)
    {
        var result = await _appUserService.CreateAppUserAsync(model, cancellationToken);

        return result.Convert();
    }

    [HttpPatch]
    [Authorize]
    [Route("password")]
    public async Task<IActionResult> ResetPasswordAsync(CancellationToken cancellationToken, [FromBody] PasswordUpdateModel model)
    {
        var result = await _appUserService.UpdateAppUserPasswrodAsync(this.GetCurrentUserId(), model, cancellationToken);

        return result.Convert();
    }

    [HttpPatch]
    [Authorize]
    [Route("user-data")]
    public async Task<IActionResult> PatchUserDataAsync(CancellationToken cancellationToken, [FromBody] AppUserUpdateModel model)
    {
        var result = await _appUserService.UpdateAppUserAsync(this.GetCurrentUserId(), model, cancellationToken);
        
        return result.Convert();
    }
}
