using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;
using Stock.Application.DTO;
using Cafe.Application.Interfaces;
using AutoMapper;
using System.Net;
using MediatR;
using MassTransit;

namespace Cafe.Application.UseCases.BillCases.Create;

public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, IOperationResult>
{
    private readonly IMapper _mapper;
    private readonly IBillRepository _billRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IPublishEndpoint _publishEndPoint;

    public CreateBillCommandHandler(IMapper mapper, 
                                    IBillRepository repository,  
                                    IPublishEndpoint publishEndPoint,
                                    IDishRepository dishRepository)
    {
        _mapper = mapper;
        _billRepository = repository;
        _publishEndPoint = publishEndPoint;
        _dishRepository = dishRepository;
    }

    public async Task<IOperationResult> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var bill = _mapper.Map<CreateBillCommand, Bill>(request);
        bill.DateTime = DateTime.Now;
        bill = await _billRepository.CreateAsync(bill, cancellationToken);
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
            var dish = await _dishRepository.GetByIdAsync(pair.First, cancellationToken);

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
        
        await _publishEndPoint.Publish<TransactionCreationDTO>(message);
    }
}