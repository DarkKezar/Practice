using Cafe.Application.AutoMappers;
using Cafe.Application.Interfaces;
using Cafe.Application.Proto;
using Cafe.Application.Services;
using Cafe.Application.Validators;
using Cafe.Infrastructure.Data.DBContext;
using Cafe.Infrastructure.Data.Repositories;
using FluentValidation;
using MongoDB.Driver;

namespace Cafe.Web.StartUps;

public class GrpcServerStartup
{
    private readonly IConfiguration _configuration;

    public GrpcServerStartup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc();
        services.Configure<CafeDatabaseSettings>(
            _configuration.GetSection("CafeDatabase"));
        services.AddSingleton<IMongoClient>(s => 
            new MongoClient(_configuration.GetSection("CafeDatabase")["ConnectionString"])
        );
        services.AddSingleton<AppDbContext>();
        services.AddAutoMapper(typeof(EmployeeProfile));
        services.AddScoped<IValidator<AccountRequest>, AccountRequestValidator>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<AccountCreationService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<AccountCreationService>();
        });
    }
}