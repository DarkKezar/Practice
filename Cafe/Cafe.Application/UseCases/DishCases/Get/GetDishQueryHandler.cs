using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Application.Exceptions;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetDishQueryHandler : IRequestHandler<GetDishQuery, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public GetDishQueryHandler(IMapper mapper, IDishRepository repository)
        => (_mapper, _dishRepository) = (mapper, repository);

    public async Task<IOperationResult> Handle(GetDishQuery request, CancellationToken cancellationToken)
    {
        var dish = await _dishRepository.GetByIdAsync((Guid)request.Id, cancellationToken);
        if(dish == null) 
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        }
        var result = _mapper.Map<Dish, GetDishQueryResponse>(dish);
        
        return new OperationResult<GetDishQueryResponse>(Messages.Success, HttpStatusCode.OK, result);
    }
}
