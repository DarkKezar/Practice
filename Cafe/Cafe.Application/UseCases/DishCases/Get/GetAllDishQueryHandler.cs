using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Exceptions;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetAllDishQueryHandler : IRequestHandler<GetAllDishQuery, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public GetAllDishQueryHandler(IMapper mapper, IDishRepository repository)
        => (_mapper, _dishRepository) = (mapper, repository);

    public async Task<IOperationResult> Handle(GetAllDishQuery request, CancellationToken cancellationToken)
    {
        if(request.Page < 0 || request.Count < 0)
        {
            throw new OperationWebException(Messages.BadRequest, (HttpStatusCode)400);
        }
        var dish = await _dishRepository.GetAllAsync(request.Page, request.Count);
        var result = _mapper.Map<List<GetDishQueryResponse>>(dish);
        
        return new OperationResult<List<GetDishQueryResponse>>(Messages.Success, HttpStatusCode.OK, result);
    }
}
