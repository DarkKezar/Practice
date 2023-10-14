using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetDishQueryHandler : IRequestHandler<GetDishQuery, IApiResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public GetDishQueryHandler(IMapper mapper, IDishRepository repository)
        => (_mapper, _dishRepository) = (mapper, repository);

    public async Task<IApiResult> Handle(GetDishQuery request, CancellationToken cancellationToken)
    {
        var dish = await _dishRepository.GetByIdAsync((Guid)request.Id);
        if(dish == null) 
        {
            throw new Exception(Messages.NotFound);
        }
        var result = _mapper.Map<Dish, GetDishQueryResponse>(dish);
        
        return new OperationResult<GetDishQueryResponse>(Messages.Success, HttpStatusCode.OK, result);
    }
}
