using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Delete;

public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, IOperationResult>
{
    private readonly IDishRepository _dishRepository;

    public DeleteDishCommandHandler(IDishRepository repository)
    {
        _dishRepository = repository;
    }

    public async Task<IOperationResult> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        var dish = await _dishRepository.GetByIdAsync(request.Id);
        await _dishRepository.DeleteAsync(dish);

        return new OperationResult<object>(Messages.Success, HttpStatusCode.OK);
    }
}
