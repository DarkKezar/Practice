using Cafe.Web.Middlewares;
using Cafe.Web.Extenssions;
using Cafe.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.DatabaseRegistration();
builder.RepositoriesRegistration();
builder.AutomappersRegistration();
builder.ValidatorsRegistration();
builder.CommandAndQueryRegistration();

builder.Services.AddSignalR()
    .AddHubOptions<BillHub>(options =>
    {
        options.EnableDetailedErrors = true;
        options.KeepAliveInterval = TimeSpan.FromMinutes(1);
        options.ClientTimeoutInterval = TimeSpan.FromMinutes(8 * 60);
    });;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials()); 
}

app.MapHub<BillHub>("/bills");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
