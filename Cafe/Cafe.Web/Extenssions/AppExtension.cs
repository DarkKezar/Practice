namespace Cafe.Web.Extenssions;

public static class AppExtension
{
    public static void AddingCorsSettings(this WebApplication app)
    {
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()); 
    }
}