using Cafe.Application.Interfaces;
using Hangfire;

namespace Cafe.Web.Extenssions;

public static class AppExtensions
{
    public static void JobsRegistration(this WebApplication app)
    {
        RecurringJob.AddOrUpdate("ReportService", (IReportService service) => service.GenerateDailyRepostAsync(), "0 18 * * 1-5");
    }

    public static void AddingCorsSettings(this WebApplication app)
    {
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()); 
    }
}
