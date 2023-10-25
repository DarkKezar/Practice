using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Get;

public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeeQueryHandler(IMapper mapper, IEmployeeRepository repository)
        => (_mapper, _employeeRepository) = (mapper, repository);

    public async Task<IOperationResult> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        if(request.Page < 0 || request.Count < 0)
        {
            throw new OperationWebException(Messages.BadRequest, (HttpStatusCode)400);
        }
        var employee = await _employeeRepository.GetAllAsync(request.Page, request.Count, cancellationToken);
        var result = _mapper.Map<List<GetEmployeeQueryResponse>>(employee);
        
        return new OperationResult<List<GetEmployeeQueryResponse>>(Messages.Success, HttpStatusCode.OK, result);
    }
}
