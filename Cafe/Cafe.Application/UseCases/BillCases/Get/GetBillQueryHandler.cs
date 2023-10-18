using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Exceptions;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.BillCases.Get;

public class GetBillQueryHandler : IRequestHandler<GetBillQuery, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IBillRepository _billRepository;

    public GetBillQueryHandler(IMapper mapper, IBillRepository repository)
        => (_mapper, _billRepository) = (mapper, repository);

    public async Task<IOperationResult> Handle(GetBillQuery request, CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync((Guid)request.Id);
        if(bill == null)
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)400);
        } 
        var result = _mapper.Map<Bill, GetBillQueryResponse>(bill);
        
        return new OperationResult<GetBillQueryResponse>(Messages.Success, HttpStatusCode.OK, result);
    }
}
