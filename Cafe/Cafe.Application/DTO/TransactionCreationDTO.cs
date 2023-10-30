namespace Cafe.Application.DTO;

public class TransactionCreationDTO
{
    public Guid UserId { get; set; }
    public List<Guid> IngridientsId { get; set; }
    public List<double> Count { get; set; }
}
