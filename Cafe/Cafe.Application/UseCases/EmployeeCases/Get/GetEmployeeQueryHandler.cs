using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Exceptions;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Get;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeQueryHandler(IMapper mapper, IEmployeeRepository repository)
        => (_mapper, _employeeRepository) = (mapper, repository);

    public async Task<IOperationResult> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync((Guid)request.Id, cancellationToken);
        if(employee == null)
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        } 
        var result = _mapper.Map<Employee, GetEmployeeQueryResponse>(employee);
        
        return new OperationResult<GetEmployeeQueryResponse>(Messages.Success, HttpStatusCode.OK, result);
    }
}
