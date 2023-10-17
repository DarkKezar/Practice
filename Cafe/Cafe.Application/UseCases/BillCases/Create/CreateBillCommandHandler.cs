using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Application.UseCases.DishCases.Get;
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
    private readonly IMessageBrokerService _messageBroker;
    private readonly IMediator _mediator;

    public CreateBillCommandHandler(IMapper mapper, 
                                    IBillRepository repository, 
                                    IValidator<CreateBillCommand> validator, 
                                    IMessageBrokerService messageBroker,
                                    IMediator mediator)
    {
        _mapper = mapper;
        _billRepository = repository;
        _validator = validator;
        _messageBroker = messageBroker;
        _mediator = mediator;
    }

    public async Task<IOperationResult> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if(!validationResult.IsValid)
        {
            throw new OperationWebException(validationResult.ToString("|"), (HttpStatusCode)400);
        }

        var bill = _mapper.Map<CreateBillCommand, Bill>(request);
        bill.DateTime = DateTime.Now;
        bill = await _billRepository.CreateAsync(bill);
        await SendMessage(bill, cancellationToken);

        return new OperationResult<Bill>(Messages.Created, HttpStatusCode.Created, bill);
    }
    
    private async Task SendMessage(Bill bill, CancellationToken cancellationToken)
    {
        var message = new TransactionCreationDTO() 
            { 
                UserId = new Guid(), 
                IngridientsId = new List<Guid>(),
                Count = new List<double>()
            };

        foreach (var pair in bill.Dishes)
        {
            var dish = ((OperationResult<GetDishQueryResponse>)(await _mediator.Send(new GetDishQuery(pair.First)))).ObjectResult;

            foreach (var ingridient in dish.Ingridients)
            {
                var index = message.IngridientsId.IndexOf(ingridient.First);
                if(index == -1)
                {
                    message.IngridientsId.Add(ingridient.First);
                    message.Count.Add(-ingridient.Second * pair.Second);
                }
                else 
                {
                    message.Count[index] -= ingridient.Second * pair.Second;
                }
            }
        }
        _messageBroker.SendMessage(message);
    }
}