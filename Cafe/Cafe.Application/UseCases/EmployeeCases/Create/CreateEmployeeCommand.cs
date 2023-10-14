using MediatR;
using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;

namespace Cafe.Application.UseCases.EmployeeCases.Create;

public class CreateEmployeeCommand : IRequest<IApiResult>
{
    public Guid IdentityId { get; set; }
    public string Biography { get; set; }
    public decimal Salary { get; set; }
}
