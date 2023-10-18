namespace Cafe.Application.UseCases.EmployeeCases.Get;

public class GetEmployeeQueryResponse
{
    public Guid IdentityId { get; set; }
    public string Biography { get; set; }
    public decimal Salsry { get; set; }
}
