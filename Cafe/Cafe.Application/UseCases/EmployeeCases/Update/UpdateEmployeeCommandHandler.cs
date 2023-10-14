using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;
using FluentValidation;

namespace Cafe.Application.UseCases.EmployeeCases.Update;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IApiResult>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<UpdateEmployeeCommand> _validator;

    public UpdateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository repository, IValidator<UpdateEmployeeCommand> validator)
    {
        _mapper = mapper;
        _employeeRepository = repository;
        _validator = validator;
    }

    public async Task<IApiResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if(!validationResult.IsValid)
        {
            throw new Exception(validationResult.ToString("|"));
        }
        var employee = await _employeeRepository.GetByIdAsync(request.Id);
        _mapper.Map(request, employee);
        await _employeeRepository.UpdateAsync(employee);

        return new OperationResult<object>(Messages.Success, HttpStatusCode.OK);
    }
}
