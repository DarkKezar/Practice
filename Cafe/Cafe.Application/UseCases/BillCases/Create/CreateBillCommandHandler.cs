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

namespace Cafe.Application.UseCases.BillCases.Create;

public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IBillRepository _billRepository;
    private readonly IValidator<CreateBillCommand> _validator;

    public CreateBillCommandHandler(IMapper mapper, IBillRepository repository, IValidator<CreateBillCommand> validator)
    {
        _mapper = mapper;
        _billRepository = repository;
        _validator = validator;
    }

    public async Task<IOperationResult> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new OperationWebException(validationResult.ToString("|"), (HttpStatusCode)400);
        }

        var bill = _mapper.Map<CreateBillCommand, Bill>(request);
        bill.DateTime = DateTime.Now;
        bill = await _billRepository.CreateAsync(bill);

        return new OperationResult<Bill>(Messages.Created, HttpStatusCode.Created, bill);
    }
}
