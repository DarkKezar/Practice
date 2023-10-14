using System.Collections.Generic;
using Cafe.Application.ApiResult;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetDishQuery : IRequest<IApiResult>
{
    public Guid? Id { get; set; }

    public GetDishQuery(Guid id) => Id = id;
}
