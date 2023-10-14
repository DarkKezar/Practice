using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Get;

public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, IApiResult>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeeQueryHandler(IMapper mapper, IEmployeeRepository repository)
        => (_mapper, _employeeRepository) = (mapper, repository);

    public async Task<IApiResult> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = (await _employeeRepository.GetAllAsync())
                    .Skip(request.Page * request.Count)
                    .Take(request.Count)
                    .ToList();
        var result = _mapper.Map<List<GetEmployeeQueryResponse>>(employee);
        
        return new OperationResult<List<GetEmployeeQueryResponse>>(Messages.Success, HttpStatusCode.OK, result);
    }
}
