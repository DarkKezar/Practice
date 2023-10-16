using System.Collections.Generic;
using Cafe.Application.OperationResult;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetAllDishQuery : IRequest<IOperationResult>
{
    public int Page { get; set; } = 0;
    public int Count { get; set; } = 0;

    public GetAllDishQuery(int page, int count)
        => (Page, Count) = (page, count);
}
