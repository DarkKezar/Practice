using Cafe.Application.Interfaces;

namespace Cafe.Application.Services;

public class RepostService : IReportService
{
    private readonly IBillRepository _billRepository;

    public RepostService(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task GenerateDailyRepostAsync()
    {
        var bills = await _billRepository.GetDailyBillsAsync();
        //logic
        //sending results;

        Console.WriteLine(bills.Count);
    }
}
