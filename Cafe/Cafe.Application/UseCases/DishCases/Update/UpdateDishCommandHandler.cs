using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;
using FluentValidation;

namespace Cafe.Application.UseCases.DishCases.Update;

public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, IApiResult>
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

    public async Task<IApiResult> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if(!validationResult.IsValid)
        {
            throw new Exception(validationResult.ToString("|"));
        }
        var dish = await _dishRepository.GetByIdAsync(request.Id);
        _mapper.Map(request, dish);
        await _dishRepository.UpdateAsync(dish);

        return new OperationResult<object>(Messages.Success, HttpStatusCode.OK);
    }
}
