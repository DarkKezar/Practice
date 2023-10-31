using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Identity.DAL.Repositories.AppUserRepository;
using Identity.DAL.Repositories.AppRoleRepository;
using Identity.BLL.Services.AppUserService;
using Identity.BLL.Services.AppRoleService;
using Identity.BLL.Services.TokenService;
using Identity.BLL.Services.AuthService;
using Identity.BLL.AutoMappers;
using Identity.DAL.Models;
using Identity.DAL.Context;
using Identity.BLL.DTO;
using Microsoft.EntityFrameworkCore;
using Grpc.Net.Client;
using static Identity.DLL.Proto.AccountCreation;
using Identity.DLL.Proto;

namespace Identity.Web.Extensions;

public static class BuilderExtension
{
    public static void GrpcClientRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<AccountCreationClient>(c => {
            return new AccountCreation.AccountCreationClient(
                GrpcChannel.ForAddress(builder.Configuration.GetSection("gRPC")["Address"])
            );
        });
    }

    public static void DatabaseRegistration(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext<AppIdentityContext>
                (options => options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
        builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppIdentityContext>();    
    }

    public static void RepositoriesRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
        builder.Services.AddTransient<IAppRoleRepository, AppRoleRepository>(); 
    }
    
    public static void ServicesRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService, TokenService>();    
        builder.Services.AddScoped<IAppUserService, AppUserService>();
        builder.Services.AddScoped<IAppRoleService, AppRoleService>();
        builder.Services.AddScoped<IAuthService, AuthService>();    
    }
    public static void AutomappersRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(UserProfile));
        builder.Services.AddAutoMapper(typeof(RoleProfile));
    }

    public static void AnotherDIRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JWTConfig>(
            builder.Configuration.GetSection("Jwt"));
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
