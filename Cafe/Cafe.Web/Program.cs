using Cafe.Web.Middlewares;
using Cafe.Web.Extenssions;
using Hangfire;
using Cafe.Web.Hubs;
using Cafe.Application.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.DatabaseRegistration();
builder.MessageBrokerRegistration();
builder.RepositoriesRegistration();
builder.AutomappersRegistration();
builder.ValidatorsRegistration();
builder.CommandAndQueryRegistration();
builder.HangfireRegistration();
builder.SignalRRegistration();
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
    app.UseHangfireDashboard();
    app.AddingCorsSettings();
}

app.MapHub<BillHub>("/bills");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
app.JobsRegistration();
app.MapGrpcService<AccountCreationService>();
app.Run();


