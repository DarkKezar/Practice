using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Exceptions;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;
using FluentValidation;

namespace Cafe.Application.UseCases.EmployeeCases.Create;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<CreateEmployeeCommand> _validator;

    public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository repository, IValidator<CreateEmployeeCommand> validator)
    {
        _mapper = mapper;
        _employeeRepository = repository;
        _validator = validator;
    }

    public async Task<IOperationResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new OperationWebException(validationResult.ToString("|"), (HttpStatusCode)400);
        }
        var employee = _mapper.Map<CreateEmployeeCommand, Employee>(request);
        employee = await _employeeRepository.CreateAsync(employee);

        return new OperationResult<Employee>(Messages.Created, HttpStatusCode.Created, employee);
    }
}
