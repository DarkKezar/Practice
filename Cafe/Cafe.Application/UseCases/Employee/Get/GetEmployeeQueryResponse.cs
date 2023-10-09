namespace Cafe.Application.UseCases.Employee.Get;

public class GetEmployeeQueryResponse
{
    public Guid IdentityId { get; set; }
    public string Biography { get; set; }
    public decimal Salsry { get; set; }
}
