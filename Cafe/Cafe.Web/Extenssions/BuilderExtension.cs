using Cafe.Infrastructure.Data.DBContext;
using Cafe.Infrastructure.Data.Repositories;
using Cafe.Application.Interfaces;
using Cafe.Application.UseCases.BillCases.Create;
using Cafe.Application.UseCases.BillCases.Get;
using Cafe.Application.UseCases.DishCases.Create;
using Cafe.Application.UseCases.DishCases.Delete;
using Cafe.Application.UseCases.DishCases.Get;
using Cafe.Application.UseCases.DishCases.Update;
using Cafe.Application.UseCases.EmployeeCases.Create;
using Cafe.Application.UseCases.EmployeeCases.Get;
using Cafe.Application.UseCases.EmployeeCases.Update;
using Cafe.Application.Validators;
using Cafe.Application.AutoMappers;
using Cafe.Web.Hubs;
using System.Text;
using Microsoft.OpenApi.Models;
using FluentValidation;
using MediatR;
using MongoDB.Driver;
using Hangfire;
using Hangfire.PostgreSql;
using Cafe.Application.Services;
using Cafe.Application.Proto;
using Cafe.Application.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MassTransit;

namespace Cafe.Web.Extenssions;

public static class BuilderExtension
{
    public static void HangfireRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(config => {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(builder.Configuration.GetSection("Hangfire")["DefaultConnection"]);
        });

        builder.Services.AddHangfireServer();  
    }
     
    public static void SignalRRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR()
        .AddHubOptions<BillHub>(options =>
        {
            options.EnableDetailedErrors = true;
            options.KeepAliveInterval = TimeSpan.FromMinutes(1);
            options.ClientTimeoutInterval = TimeSpan.FromMinutes(8 * 60);
        }
    }
    
    public static void ConfigureKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(int.Parse(builder.Configuration.GetSection("Ports")["Http1"]), o => o.Protocols = HttpProtocols.Http1);
            options.ListenAnyIP(int.Parse(builder.Configuration.GetSection("Ports")["Http2"]), o => o.Protocols = HttpProtocols.Http2);
        });
    }

    public static void DatabaseRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CafeDatabaseSettings>(
            builder.Configuration.GetSection("CafeDatabase"));
        builder.Services.AddSingleton<AppDbContext>();
    }

    public static void MessageBrokerRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(mt => mt.AddMassTransit(x => {
            x.UsingRabbitMq((cntxt, cfg) => {
                cfg.Host(builder.Configuration.GetSection("RabbitMQ")["HostName"], builder.Configuration.GetSection("RabbitMQ")["VHost"], c => {
                    c.Username(builder.Configuration.GetSection("RabbitMQ")["User"]);
                    c.Password(builder.Configuration.GetSection("RabbitMQ")["Password"]);
                    
                });
            });
        }));
    }

    public static void RepositoriesRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IBillRepository, BillRepository>();
        builder.Services.AddTransient<IDishRepository, DishRepository>();
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddTransient<IReportRepository, ReportRepository>();
    }

    public static void CommandAndQueryRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportService, ReportService>();
        builder.Services.AddScoped<AccountCreationService>();
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBillCommandHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBillQueryHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBillQueryHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDishCommandHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetDishQueryHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllDishQueryHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteDishCommandHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateDishCommandHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommandHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetEmployeeQueryHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEmployeeQueryHandler).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateEmployeeCommandHandler).Assembly)); 
    }

    public static void AutomappersRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(BillProfile));
        builder.Services.AddAutoMapper(typeof(DishProfile));
        builder.Services.AddAutoMapper(typeof(EmployeeProfile));
    }

    public static void ValidatorsRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IValidator<AccountRequest>, AccountRequestValidator>();
        builder.Services.AddScoped<IValidator<CreateBillCommand>, CreateBillCommandValidator>();
        builder.Services.AddScoped<IValidator<CreateDishCommand>, CreateDishCommandValidator>();
        builder.Services.AddScoped<IValidator<CreateEmployeeCommand>, CreateEmployeeCommandValidator>();
        builder.Services.AddScoped<IValidator<UpdateDishCommand>, UpdateDishCommandValidator>();
        builder.Services.AddScoped<IValidator<UpdateEmployeeCommand>, UpdateEmployeeCommandValidator>();
    }

    public static void SwaggerSetUp(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
}
