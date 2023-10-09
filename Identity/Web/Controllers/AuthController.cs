using BLL.DTO;
using DAL.Models;
using BLL.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Web.Extensions;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, 
                            IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> AuthAsync([FromBody]AuthModel model)
    {
        return (await _authService.AuthAccountAsync(model, _configuration.GetJWTConfig())).Convert();
    }
}
