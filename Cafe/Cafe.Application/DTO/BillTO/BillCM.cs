using Cafe.Domain.Entities;

namespace Cafe.Application.DTO.BillTO;

public class BillCM
{
    public Pair<Guid, double> Dishes { get; set; }
    public double Sale { get; set; }
}
