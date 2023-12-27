using Microsoft.AspNetCore.Mvc;
using Identity.BLL.Services.AppUserService;
using Identity.BLL.DTO;
using Identity.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Identity.BLL.OperationResult;
using Newtonsoft.Json;

namespace Identity.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IDistributedCache _cache;


    public UserController(IAppUserService appUserService, IDistributedCache cache)
    {
        _appUserService = appUserService;
        _cache = cache;
    }

    [HttpGet]
    [Route("{email}")]
    public async Task<IActionResult> GetUserAsync(CancellationToken cancellationToken, string email)
    {
        var cacheString = $"transactions/{email}";
        var cacheResult = await _cache.GetStringAsync(cacheString, cancellationToken);
        IOperationResult result;
        if(cacheResult == null)
        {
            result = await _appUserService.GetAppUserAsync(email, cancellationToken);
            await _cache.SetStringAsync( cacheString, 
                                        JsonConvert.SerializeObject(result, Formatting.Indented), 
                                        new DistributedCacheEntryOptions 
                                        { 
                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) 
                                        }, 
                                        cancellationToken);
        }
        else
        {
            result = JsonConvert.DeserializeObject<OperationResult<GetAppUserDTO>>(cacheResult);
        }

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int count = 10)
    {
        var cacheString = $"transactions/{page}:{count}";
        var cacheResult = await _cache.GetStringAsync(cacheString, cancellationToken);
        IOperationResult result;
        if(cacheResult == null)
        {
            result = await _appUserService.GetAllAppUserAsync(page, count, cancellationToken);
            await _cache.SetStringAsync( cacheString, 
                                        JsonConvert.SerializeObject(result, Formatting.Indented), 
                                        new DistributedCacheEntryOptions 
                                        { 
                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) 
                                        }, 
                                        cancellationToken);
        }
        else
        {
            result = JsonConvert.DeserializeObject<OperationResult<IList<GetAppUserDTO>>>(cacheResult);
        }

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
