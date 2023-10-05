using Cafe.Domain.Entities;

namespace Cafe.Application.DTO.BillTO;

public class BillVM
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public Pair<Guid, double> Dishes { get; set; }
    public double Sale { get; set; }
}
