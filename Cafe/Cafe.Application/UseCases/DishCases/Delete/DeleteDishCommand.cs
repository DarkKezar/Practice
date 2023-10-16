using MediatR;
using Cafe.Application.ApiResult;

namespace Cafe.Application.UseCases.DishCases.Delete;

public class DeleteDishCommand : IRequest<IApiResult>
{
    public Guid Id { get; set; }

    public DeleteDishCommand(Guid id) => Id = id;
}
