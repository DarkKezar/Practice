using Stock.Web;
using Stock.Web.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Stock.Web.Middlewares;
using Stock.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace SUT.Stock.SetUp;

public class StockServiceSetup : IDisposable
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
        builder.RepositoriesRegistration();
        builder.ServicesRegistration();
        builder.AutomappersRegistration();
        builder.ValidatorsRegistration();
        builder.MessageBrokerRegistration();

        builder.Services.AddControllers();
        builder.Services.AddControllers().AddApplicationPart(typeof(IngridientsController).Assembly);
        builder.Services.AddControllers().AddApplicationPart(typeof(TransactionsController).Assembly);
        builder.Services.AddEndpointsApiExplorer();
        builder.SwaggerSetUp();

        _app = builder.Build();
    
        _app.UseHttpsRedirection();
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
