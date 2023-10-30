using Identity.Web.Extensions;
using Identity.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.GrpcClientRegistration();
builder.DatabaseRegistration();
builder.RepositoriesRegistration();
builder.ServicesRegistration();
builder.AutomappersRegistration();
builder.AnotherDIRegistration();

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
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
