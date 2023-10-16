using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.BillCases.Get;

public class GetAllBillQueryHandler : IRequestHandler<GetAllBillQuery, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IBillRepository _billRepository;

    public GetAllBillQueryHandler(IMapper mapper, IBillRepository repository)
        => (_mapper, _billRepository) = (mapper, repository);

    public async Task<IOperationResult> Handle(GetAllBillQuery request, CancellationToken cancellationToken)
    {
        if(request.Page < 0 || request.Count < 0)
        {
            throw new OperationWebException(Messages.BadRequest, (HttpStatusCode)400);
        }
        var bill = await _billRepository.GetAllAsync(request.Page, request.Count);
        var result = _mapper.Map<List<GetBillQueryResponse>>(bill);
        
        return new OperationResult<List<GetBillQueryResponse>>(Messages.Success, HttpStatusCode.OK, result);
    }
}
