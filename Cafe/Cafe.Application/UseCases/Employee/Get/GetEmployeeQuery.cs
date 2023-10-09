using System.Collections.Generic;
using Cafe.Domain.Entities;
using MediatR;

namespace Cafe.Application.UseCases.Employee.Get;

public class GetEmployeeQuery : IRequest<List<GetEmployeeQueryResponse>>
{
    public Guid? Id { get; set; }
    public Pair<int, int>? Pagination { get; set; }

    public GetEmployeeQuery(Guid id) => (Id, Pagination) = (id, null);
    public GetEmployeeQuery(Pair<int,int> pagination) => (Id, Pagination) = (null, pagination);
}
