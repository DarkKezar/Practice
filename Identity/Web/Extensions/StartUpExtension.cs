using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DAL.Repositories.AppUserRepository;
using DAL.Repositories.AppRoleRepository;
using BLL.Services.AppUserService;
using BLL.Services.AppRoleService;
using BLL.Services.AuthService;
using BLL.AutoMappers;
using DAL.Models;
using DAL.Context;

namespace Web.Extensions;

public static class StartUpExtension
{
    public static void DependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(UserProfile));
        builder.Services.AddIdentity<AppUser, AppRole>() .AddEntityFrameworkStores<AppIdentityContext>();

        builder.Services.AddScoped<IAppUserService, AppUserService>();
        builder.Services.AddScoped<IAppRoleService, AppRoleService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

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
