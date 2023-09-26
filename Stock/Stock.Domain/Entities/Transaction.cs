namespace Stock.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid UserId { get; set; }
    public List<Guid> IngridientsId { get; set; }
    public List<double> Count { get; set; }
    /*
        Count < 0 => ingridient[i] was taken away
        Count = 0 => ingridient[i] hadn't changes
        Count > 0 => ingridient[i] was added
    */
    public DateTime DateTime { get; set; }
}
