using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.BillCases.Get;

public class GetAllBillQueryHandler : IRequestHandler<GetAllBillQuery, IApiResult>
{
    private readonly IMapper _mapper;
    private readonly IBillRepository _billRepository;

    public GetAllBillQueryHandler(IMapper mapper, IBillRepository repository)
        => (_mapper, _billRepository) = (mapper, repository);

    public async Task<IApiResult> Handle(GetAllBillQuery request, CancellationToken cancellationToken)
    {
        
        var bill = (await _billRepository.GetAllAsync())
                    .Skip(request.Page * request.Count)
                    .Take(request.Count)
                    .ToList();
        var result = _mapper.Map<List<GetBillQueryResponse>>(bill);
        
        return new OperationResult<List<GetBillQueryResponse>>(Messages.Success, HttpStatusCode.OK, result);
    }
}
