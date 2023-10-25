using Cafe.Web.Middlewares;
using Cafe.Web.Extenssions;
using Cafe.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.DatabaseRegistration();
builder.RepositoriesRegistration();
builder.AutomappersRegistration();
builder.ValidatorsRegistration();
builder.CommandAndQueryRegistration();
builder.SignalRRegistration();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.AddingCorsSettings();
}

app.MapHub<BillHub>("/bills");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
