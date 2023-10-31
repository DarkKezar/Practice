using Microsoft.OpenApi.Models;
using Stock.Infrastructure.Data.DBContext;
using Stock.Application.IServices;
using Stock.Application.Services;
using Stock.Application.Interfaces;
using Stock.Infrastructure.Data.Repositories;
using Stock.Application.Automappers;
using Stock.Application.Validators;
using Stock.Application.DTO;
using FluentValidation;
using MassTransit;
using Stock.Web.Consumers;
using MongoDB.Driver;

namespace Stock.Web.Extensions;

public static class BuilderExtensions
{
    public static void DatabaseRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<StockDatabaseSettings>(
            builder.Configuration.GetSection("StockDatabase"));
        builder.Services.AddSingleton<IMongoClient>(s => 
             new MongoClient(builder.Configuration.GetSection("StockDatabase")["ConnectionString"])
         );
        builder.Services.AddSingleton<AppDbContext>();
    }

    [Obsolete]
    public static void MessageBrokerRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<CommandMessageConsumer>();
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(builder.Configuration.GetSection("RabbitMQ")["HostName"], builder.Configuration.GetSection("RabbitMQ")["VHost"], h => { });

                cfg.ReceiveEndpoint("TransactionCreationDTO", ep =>
                {
                    ep.ConfigureConsumer<CommandMessageConsumer>(provider); 
                });
            }));
        });
    }
    
    public static void RepositoriesRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IIngridientRepository, IngridientRepository>();
        builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
    }

    public static void ServicesRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IIngridientService, IngridientService>();
        builder.Services.AddTransient<ITransactionService, TransactionService>(); 
    }

    public static void AutomappersRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(IngridientProfile));
        builder.Services.AddAutoMapper(typeof(TransactionProfile));
    }

    public static void ValidatorsRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IValidator<IngridientCreationDTO>, IngridientCreationDTOValidator>();
        builder.Services.AddTransient<IValidator<TransactionCreationDTO>, TransactionCreationDTOValidator>(); 
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
