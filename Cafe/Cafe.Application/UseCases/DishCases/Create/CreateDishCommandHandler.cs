using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Exceptions;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;
using FluentValidation;

namespace Cafe.Application.UseCases.DishCases.Create;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;
    private readonly IValidator<CreateDishCommand> _validator;

    public CreateDishCommandHandler(IMapper mapper, IDishRepository repository, IValidator<CreateDishCommand> validator)
    {
        _mapper = mapper;
        _dishRepository = repository;
        _validator = validator;
    }

    public async Task<IOperationResult> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new OperationWebException(validationResult.ToString("|"), (HttpStatusCode)400);
        }
        var dish = _mapper.Map<CreateDishCommand, Dish>(request);
        dish = await _dishRepository.CreateAsync(dish);

        return new OperationResult<Dish>(Messages.Created, HttpStatusCode.Created, dish);
    }
}
