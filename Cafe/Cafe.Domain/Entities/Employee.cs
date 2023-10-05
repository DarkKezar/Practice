namespace Cafe.Domain.Entities;

public class Employee : BaseEntity
{
    public Guid IdentityId { get; set; }
    public string Biography { get; set; }
    public decimal Salsry { get; set; }
}
