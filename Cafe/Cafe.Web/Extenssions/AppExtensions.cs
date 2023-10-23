using Cafe.Application.Interfaces;
using Hangfire;

namespace Cafe.Web.Extenssions;

public static class AppExtensions
{
    public static void JobsRegistration(this WebApplication app)
    {
        //At 06:00 PM, Monday through Friday
        RecurringJob.AddOrUpdate("ReportService", (IReportService service) => service.GenerateDailyRepostAsync(), "0 18 * * 1-5");
    }
}
