using Cafe.Web.Middlewares;
using Cafe.Web.Extenssions;
using Hangfire;
using Cafe.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.DatabaseRegistration();
builder.MessageBrokerRegistration();
builder.RepositoriesRegistration();
builder.AutomappersRegistration();
builder.ValidatorsRegistration();
builder.CommandAndQueryRegistration();
builder.HangfireRegistration();

builder.Services.AddControllers();
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
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
app.JobsRegistration();
app.MapGrpcService<AccountCreationService>();
app.Run();


