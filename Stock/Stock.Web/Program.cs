using Stock.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.DatabaseRegistration();
builder.RepositoriesRegistration();
builder.ServicesRegistration();
builder.AutomappersRegistration();
builder.ValidatorsRegistration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.SwaggerSetUp();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
