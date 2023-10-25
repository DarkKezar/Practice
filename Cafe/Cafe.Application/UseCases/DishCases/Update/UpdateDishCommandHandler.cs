using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Update;

public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public UpdateDishCommandHandler(IMapper mapper, IDishRepository repository)
    {
        _mapper = mapper;
        _dishRepository = repository;
    }

    public async Task<IOperationResult> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = await _dishRepository.GetByIdAsync(request.Id, cancellationToken);
        _mapper.Map(request, dish);
        await _dishRepository.UpdateAsync(dish);

        return new OperationResult<object>(Messages.Success, HttpStatusCode.OK);
    }
}
