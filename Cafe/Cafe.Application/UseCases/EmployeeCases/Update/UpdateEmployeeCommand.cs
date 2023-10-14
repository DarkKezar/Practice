using MediatR;
using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;

namespace Cafe.Application.UseCases.EmployeeCases.Update;

public class UpdateEmployeeCommand : IRequest<IApiResult>
{
    public Guid Id { get; set; }
    public string Biography { get; set; }
    public decimal Salary { get; set; }
}
