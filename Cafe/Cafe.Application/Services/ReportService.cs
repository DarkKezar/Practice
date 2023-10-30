using Cafe.Application.Interfaces;
using Cafe.Application.Extenssions;
using OfficeOpenXml;

namespace Cafe.Application.Services;

public class ReportService : IReportService
{
    private readonly IBillRepository _billRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IReportRepository _reportRepository;
    private string _fileName = "Report" + DateTime.Now.ToString("dd-MM-yy") + ".xlsx";
    private const string _billsSheetName = "Bills per day";
    private const string _ingridientsSheetName = "Ingridients per day";
    private const int _i = 2;
    private const int _j = 2;

    public ReportService(IBillRepository billRepository, IDishRepository dishRepository, IReportRepository reportRepository)
    {
        _billRepository = billRepository;
        _dishRepository = dishRepository;
        _reportRepository = reportRepository;
    }

    public async Task GenerateDailyRepostAsync()
    {
        var bills = _billRepository.GetDailyBills();

        var package = new ExcelPackage();
        var billsSheet = package.Workbook.Worksheets.Add(_billsSheetName);
        var ingridientsSheet = package.Workbook.Worksheets.Add(_ingridientsSheetName);

        var ingridients = await billsSheet.WriteBills(bills.ToList(), _dishRepository, _i, _j);
        ingridientsSheet.WriteIngridients(ingridients, _i, _j);

        await _reportRepository.RecordFileAsync(_fileName, package.GetAsByteArray());
    }
}
