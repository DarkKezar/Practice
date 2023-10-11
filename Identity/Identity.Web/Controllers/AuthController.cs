using Identity.BLL.DTO;
using Identity.BLL.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> AuthAsync([FromBody]AuthModel model)
    {
        return (await _authService.AuthAccountAsync(model)).Convert();
    }
}
