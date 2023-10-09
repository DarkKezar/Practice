using System.Collections.Generic;
using Cafe.Domain.Entities;
using MediatR;

namespace Cafe.Application.UseCases.Bill.Get;

public class GetBillQuery : IRequest<List<GetBillQueryResponse>>
{
    public Guid? Id { get; set; }
    public Pair<int, int>? Pagination { get; set; }

    public GetBillQuery(Guid id) => (Id, Pagination) = (id, null);
    public GetBillQuery(Pair<int,int> pagination) => (Id, Pagination) = (null, pagination);
}
