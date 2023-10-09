using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Cafe.Application.UseCases.Bill.Get;

public class GetBillQueryHandler : IRequestHandler<GetBillQuery, List<GetBillQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Bill> _billRepository;

    public GetBillQueryHandler(IMapper mapper, IRepository<Bill> repository)
        => (_mapper, _billRepository) = (mapper, repository);

    public async Task<List<GetBillQueryResponse>> Handle(GetBillQuery request, CancellationToken cancellationToken)
    {
        if(request.Id != null){
            var bill = await _billRepository.GetByIdAsync(request.Id);
            var result = _mapper.Map<List<GetBillQueryResponse>>(bill);
            
            return result;
        }else if(request.Pagination != null)
        {
            var bill = (await _billRepository.GetAllAsync())
                        .Skip(request.Pagination.First * request.Pagination.Second)
                        .Take(request.Pagination.Second)
                        .ToList();
            var result = _mapper.Map<List<GetBillQueryResponse>>(bill);
            
            return result;
        }else return null;
    }
}
