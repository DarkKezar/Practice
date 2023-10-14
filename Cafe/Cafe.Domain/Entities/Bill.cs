namespace Cafe.Domain.Entities;

public class Bill : BaseEntity
{
    public DateTime DateTime { get; set; }
    public List<Pair<Guid, double>> Dishes { get; set; }
    public double Sale { get; set; }
}
