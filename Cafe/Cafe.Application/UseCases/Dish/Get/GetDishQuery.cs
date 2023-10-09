using System.Collections.Generic;
using Cafe.Domain.Entities;
using MediatR;

namespace Cafe.Application.UseCases.Dish.Get;

public class GetDishQuery : IRequest<List<GetDishQueryResponse>>
{
    public Guid? Id { get; set; }
    public Pair<int, int>? Pagination { get; set; }

    public GetDishQuery(Guid id) => (Id, Pagination) = (id, null);
    public GetDishQuery(Pair<int,int> pagination) => (Id, Pagination) = (null, pagination);
}
