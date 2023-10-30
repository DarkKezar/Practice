using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Create;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public CreateDishCommandHandler(IMapper mapper, IDishRepository repository)
    {
        _mapper = mapper;
        _dishRepository = repository;
    }

    public async Task<IOperationResult> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = _mapper.Map<CreateDishCommand, Dish>(request);
        dish = await _dishRepository.CreateAsync(dish, cancellationToken);

        return new OperationResult<Dish>(Messages.Created, HttpStatusCode.Created, dish);
    }
}
