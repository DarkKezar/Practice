using MediatR;
using Cafe.Application.OperationResult;

namespace Cafe.Application.UseCases.DishCases.Delete;

public class DeleteDishCommand : IRequest<IOperationResult>
{
    public Guid Id { get; set; }

    public DeleteDishCommand(Guid id) => Id = id;
}
