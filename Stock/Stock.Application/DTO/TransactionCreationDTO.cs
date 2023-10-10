namespace Stock.Application.DTO;

public class TransactionCreationDTO
{
    //When I connect with IdentityService this line would be deleted;
    public Guid UserId { get; set; }
    public List<Guid> IngridientsId { get; set; }
    public List<double> Count { get; set; }
}
