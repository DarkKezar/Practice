using Cafe.Domain.Entities;

namespace Cafe.Application.UseCases.BillCases.Get;

public class GetBillQueryResponse
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public List<Pair<Guid, double>> Dishes { get; set; }
    public double Sale { get; set; }
}
