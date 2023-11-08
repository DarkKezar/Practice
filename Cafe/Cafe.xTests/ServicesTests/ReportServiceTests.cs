using Cafe.Application.Interfaces;
using Cafe.Application.Services;
using Cafe.xTests.Moqs;

namespace Cafe.xTests.ServicesTests;

public class ReportServiceTests
{
    public static IBillRepository billRepository = RepositoriesMoq.GetIBillRepository();
    public static IDishRepository dishRepository = RepositoriesMoq.GetIDishRepository();
    public static IReportRepository reportRepository = RepositoriesMoq.GetIReportRepository();
    public ReportService reportService = new ReportService(billRepository, dishRepository, reportRepository);

    [Fact]
    public async Task ReportTest1()
    {
        try
        {
            await reportService.GenerateDailyRepostAsync();
        }
        catch(Exception e)
        {
            Assert.True(false);
        }
        Assert.True(true);
    }
}
