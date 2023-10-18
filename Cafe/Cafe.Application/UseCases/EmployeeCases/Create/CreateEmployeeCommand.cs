using MediatR;
using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;

namespace Cafe.Application.UseCases.EmployeeCases.Create;

public class CreateEmployeeCommand : IRequest<IOperationResult>
{
    public Guid IdentityId { get; set; }
    public string Biography { get; set; }
    public decimal Salary { get; set; }
}
