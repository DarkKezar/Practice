using System.Net;
using System.Security.Claims;
using BLL.DTO;
using DAL.Models;
using BLL.CustomResult;
using BLL.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Web.Extensions;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;

    public AuthController(IAuthService authService, 
                            IConfiguration configuration, 
                            UserManager<AppUser> userManager)
    {
        _authService = authService;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> AuthAsync([FromBody]AuthModel model)
    {
        return (await _authService.AuthAccountAsync(model, _configuration.GetJWTConfig())).Convert();
    }
}
