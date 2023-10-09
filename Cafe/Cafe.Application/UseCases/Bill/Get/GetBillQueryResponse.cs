using Cafe.Domain.Entities;

namespace Cafe.Application.UseCases.Bill.Get;

public class GetBillQueryResponse
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public Pair<Guid, double> Dishes { get; set; }
    public double Sale { get; set; }
}
