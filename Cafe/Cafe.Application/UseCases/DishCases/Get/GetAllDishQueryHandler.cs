using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetAllDishQueryHandler : IRequestHandler<GetAllDishQuery, IApiResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public GetAllDishQueryHandler(IMapper mapper, IDishRepository repository)
        => (_mapper, _dishRepository) = (mapper, repository);

    public async Task<IApiResult> Handle(GetAllDishQuery request, CancellationToken cancellationToken)
    {
        
        var dish = (await _dishRepository.GetAllAsync())
                    .Skip(request.Page * request.Count)
                    .Take(request.Count)
                    .ToList();
        var result = _mapper.Map<List<GetDishQueryResponse>>(dish);
        
        return new OperationResult<List<GetDishQueryResponse>>(Messages.Success, HttpStatusCode.OK, result);
    }
}
