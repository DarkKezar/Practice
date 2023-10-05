using Cafe.Domain.Entities;

namespace Cafe.Application.DTO.EmployeeTO;

public class EmployeeCM
{
    public Guid IdentityId { get; set; }
    public string Biography { get; set; }
    public decimal Salsry { get; set; }
}
