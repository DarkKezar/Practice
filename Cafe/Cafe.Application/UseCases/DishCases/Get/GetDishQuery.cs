using System.Collections.Generic;
using Cafe.Application.OperationResult;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetDishQuery : IRequest<IOperationResult>
{
    public Guid? Id { get; set; }

    public GetDishQuery(Guid id) => Id = id;
}
