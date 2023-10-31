using Grpc.Core;
using Cafe.Application.Proto;
using AutoMapper;
using FluentValidation;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;

namespace Cafe.Application.Services;

public class AccountCreationService : AccountCreation.AccountCreationBase
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<AccountRequest> _validator;

    public AccountCreationService(IMapper mapper, IEmployeeRepository repository, IValidator<AccountRequest> validator)
    {
        _mapper = mapper;
        _employeeRepository = repository;
        _validator = validator;
    }

    public async override Task<AccountReply> CreateAccount(AccountRequest request, ServerCallContext context)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if(!validationResult.IsValid)
        {
            return new AccountReply() { Status = "Fail" };
        }
        var employee = _mapper.Map<Employee>(request);
        employee.IdentityId = Guid.Parse(request.IdentityIdString);
        employee = await _employeeRepository.CreateAsync(employee);

        return new AccountReply() { Status = "Succsess" };
    }
}
