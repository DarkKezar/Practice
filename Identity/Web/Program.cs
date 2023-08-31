
using Microsoft.EntityFrameworkCore;
using Core.Context;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<AppIdentityContext>
        (options => options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.DependencyInjection();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerBearer();
builder.AddJWTAuth();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
