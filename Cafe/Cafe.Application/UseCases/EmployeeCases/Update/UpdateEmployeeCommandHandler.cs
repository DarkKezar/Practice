using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using AutoMapper;
using System.Net;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Update;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository repository)
    {
        _mapper = mapper;
        _employeeRepository = repository;
    }

    public async Task<IOperationResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);
        _mapper.Map(request, employee);
        await _employeeRepository.UpdateAsync(employee);

        return new OperationResult<object>(Messages.Success, HttpStatusCode.OK);
    }
}
