using System.Collections.Generic;
using Cafe.Application.OperationResult;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Get;

public class GetEmployeeQuery : IRequest<IOperationResult>
{
    public Guid? Id { get; set; }
    public GetEmployeeQuery(Guid id) => Id = id;
}
