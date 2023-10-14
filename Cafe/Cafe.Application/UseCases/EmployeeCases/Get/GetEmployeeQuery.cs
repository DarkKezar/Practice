using System.Collections.Generic;
using Cafe.Application.ApiResult;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Get;

public class GetEmployeeQuery : IRequest<IApiResult>
{
    public Guid? Id { get; set; }
    public GetEmployeeQuery(Guid id) => Id = id;
}
