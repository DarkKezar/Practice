using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Repositories.AppUserRepository;
using Core.Repositories.AppRoleRepository;
using Infr.Services.AppUserService;
using Infr.Services.AppRoleService;
using Infr.Services.AuthService;
using Core.Models;
using Core.Context;

namespace Web.Extensions;

public static class StartUpExtension
{
    public static void DependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<AppUser, AppRole>() .AddEntityFrameworkStores<AppIdentityContext>();

        builder.Services.AddTransient<IAppUserService, AppUserService>();
        builder.Services.AddTransient<IAppRoleService, AppRoleService>();
        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
        builder.Services.AddTransient<IAppRoleRepository, AppRoleRepository>(); 
    }

    public static void AddSwaggerBearer(this WebApplicationBuilder builder)
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

    public static void AddJWTAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        builder.Services.AddAuthorization();
    }
}
