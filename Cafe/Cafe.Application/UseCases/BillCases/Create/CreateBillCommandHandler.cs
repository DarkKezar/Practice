using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using AutoMapper;
using System.Net;
using MediatR;
using FluentValidation;

namespace Cafe.Application.UseCases.BillCases.Create;

public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, IApiResult>
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

    public async Task<IApiResult> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if(validationResult.IsValid)
        {
            throw new Exception(validationResult.ToString("|"));
        }

        var bill = _mapper.Map<CreateBillCommand, Bill>(request);
        bill.DateTime = DateTime.Now;
        bill = await _billRepository.CreateAsync(bill);

        return new OperationResult<Bill>(Messages.Created, HttpStatusCode.Created, bill);
    }
}
