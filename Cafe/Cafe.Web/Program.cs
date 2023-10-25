using Cafe.Web.Middlewares;
using Cafe.Web.Extenssions;
using Cafe.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.DatabaseRegistration();
builder.RepositoriesRegistration();
builder.AutomappersRegistration();
builder.ValidatorsRegistration();
builder.CommandAndQueryRegistration();
builder.ConfigureKestrel();

builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.MapGrpcService<AccountCreationService>();
app.Run();
