using Cafe.Web.Middlewares;
using Cafe.Web.Extenssions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.DatabaseRegistration();
builder.RepositoriesRegistration();
builder.AutomappersRegistration();
builder.ValidatorsRegistration();
builder.CommandAndQueryRegistration();
builder.HangfireRegistration();

builder.Services.AddControllers();
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
app.Run();


