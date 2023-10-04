using System.Text;
using Microsoft.OpenApi.Models;
using Stock.Infrastructure.Data.DBContext;
using Stock.Application.IServices;
using Stock.Application.Services;
using Stock.Application.Interfaces;
using Stock.Infrastructure.Data.Repositories;
using Stock.Domain.Entities;

namespace Stock.Web.Extensions;

public static class BuilderExtensions
{
    public static void DependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<StockDatabaseSettings>(
            builder.Configuration.GetSection("StockDatabase"));
        builder.Services.AddSingleton<AppDbContext>();

        builder.Services.AddTransient<IIngridientService, IngridientService>();
        builder.Services.AddTransient<ITransactionService, TransactionService>();

        builder.Services.AddTransient<IRepository<Ingridient>, IngridientRepository>();
        builder.Services.AddTransient<IRepository<Transaction>, TransactionRepository>();
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
