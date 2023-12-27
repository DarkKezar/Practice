namespace Stock.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid UserId { get; set; }
    public List<Guid> IngridientsId { get; set; }
    public List<double> Count { get; set; }
    public DateTime DateTime { get; set; }
}
