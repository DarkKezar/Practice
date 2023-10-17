using Identity.BLL.DTO;
using Identity.BLL.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> AuthAsync(CancellationToken cancellationToken, [FromBody] AuthModel model)
    {
        var result = await _authService.AuthAccountAsync(model, cancellationToken);
        
        return result.Convert();
    }
}
