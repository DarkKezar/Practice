using Identity.Web;
using Identity.Web.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Identity.Web.Middlewares;
using Identity.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace SUT.Identity.SetUp;

public class IdentityServiceSetup : IDisposable
{
    private static WebApplication _app = null!;

    public static async Task<WebApplication> GetApp()
    {
        if(_app == null)
            await SetUp();

        return _app;
    }

    public static async Task SetUp()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = "SUT"
        });
        
        builder.GrpcClientRegistration();
        builder.DatabaseRegistration();
        builder.RepositoriesRegistration();
        builder.ServicesRegistration();
        builder.AutomappersRegistration();
        builder.AnotherDIRegistration();

        builder.Services.AddControllers();
        builder.Services.AddControllers().AddApplicationPart(typeof(AuthController).Assembly);
        builder.Services.AddControllers().AddApplicationPart(typeof(RoleController).Assembly);
        builder.Services.AddControllers().AddApplicationPart(typeof(UserController).Assembly);
        builder.Services.AddEndpointsApiExplorer();
        builder.AddSwaggerBearer();
        builder.AddJWTAuth();

        _app = builder.Build();
        _app.UseHttpsRedirection();
        _app.UseAuthentication();
        _app.UseAuthorization();
        _app.MapControllers();
        _app.UseMiddleware<ExceptionMiddleware>();

        await _app.StartAsync();
    }

    public async void Dispose()
    {
        if(_app != null)
            _app.DisposeAsync();
    }
}
