using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Create;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository repository)
    {
        _mapper = mapper;
        _employeeRepository = repository;
    }

    public async Task<IOperationResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<CreateEmployeeCommand, Employee>(request);
        employee = await _employeeRepository.CreateAsync(employee, cancellationToken);

        return new OperationResult<Employee>(Messages.Created, HttpStatusCode.Created, employee);
    }
}
