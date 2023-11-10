using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)    
    .AddJsonFile("configuration.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());
await app.UseOcelot();

app.Run();