using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using Cafe.Application.Exceptions;
using AutoMapper;
using System.Net;
using MediatR;
using FluentValidation;

namespace Cafe.Application.UseCases.DishCases.Update;

public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;
    private readonly IValidator<UpdateDishCommand> _validator;

    public UpdateDishCommandHandler(IMapper mapper, IDishRepository repository, IValidator<UpdateDishCommand> validator)
    {
        _mapper = mapper;
        _dishRepository = repository;
        _validator = validator;
    }

    public async Task<IOperationResult> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new OperationWebException(validationResult.ToString("|"), (HttpStatusCode)400);
        }
        var dish = await _dishRepository.GetByIdAsync(request.Id);
        _mapper.Map(request, dish);
        await _dishRepository.UpdateAsync(dish);

        return new OperationResult<object>(Messages.Success, HttpStatusCode.OK);
    }
}
