using MediatR;
using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;

namespace Cafe.Application.UseCases.EmployeeCases.Update;

public class UpdateEmployeeCommand : IRequest<IOperationResult>
{
    public Guid Id { get; set; }
    public string Biography { get; set; }
    public decimal Salary { get; set; }
}
