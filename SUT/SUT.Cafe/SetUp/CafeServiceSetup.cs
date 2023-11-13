using Cafe.Web;
using Cafe.Web.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Cafe.Web.Middlewares;
using Cafe.Web.Extenssions;
using Hangfire;
using Cafe.Web.Hubs;
using Cafe.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace SUT.Cafe.SetUp;

public class CafeServiceSetup : IDisposable
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
        
        builder.DatabaseRegistration();
        builder.MessageBrokerRegistration();
        builder.RepositoriesRegistration();
        builder.AutomappersRegistration();
        builder.ValidatorsRegistration();
        builder.CommandAndQueryRegistration();
        builder.HangfireRegistration();
        builder.SignalRRegistration();
        builder.ConfigureKestrel();

        builder.Services.AddControllers();
        builder.Services.AddControllers().AddApplicationPart(typeof(BillController).Assembly);
        builder.Services.AddControllers().AddApplicationPart(typeof(DishController).Assembly);
        builder.Services.AddControllers().AddApplicationPart(typeof(EmployeeController).Assembly);
        builder.Services.AddGrpc();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        _app = builder.Build();
        _app.MapHub<BillHub>("/bills");
        _app.UseAuthorization();
        _app.MapControllers();

        _app.UseMiddleware<ExceptionMiddleware>();
        _app.MapGrpcService<AccountCreationService>();

        await _app.StartAsync();
    }

    public async void Dispose()
    {
        if(_app != null)
            _app.DisposeAsync();
    }
}
