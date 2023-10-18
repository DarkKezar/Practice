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
using Cafe.Application.OperationResult;
using Cafe.Application.Validators;
using Cafe.Application.AutoMappers;
using System.Text;
using Microsoft.OpenApi.Models;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace Cafe.Web.Extenssions;

public static class BuilderExtension
{
    public static void DatabaseRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CafeDatabaseSettings>(
            builder.Configuration.GetSection("CafeDatabase"));
        builder.Services.AddSingleton<IMongoClient>(s => 
            new MongoClient(builder.Configuration.GetSection("CafeDatabase")["ConnectionString"])
        );
        builder.Services.AddSingleton<AppDbContext>();
    }

    public static void RepositoriesRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IBillRepository, BillRepository>();
        builder.Services.AddTransient<IDishRepository, DishRepository>();
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
    }

    public static void CommandAndQueryRegistration(this WebApplicationBuilder builder)
    {
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