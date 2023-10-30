using MassTransit;
using Stock.Application.DTO;
using Stock.Application.IServices;

namespace Stock.Web.Consumers;

public class CommandMessageConsumer : IConsumer<TransactionCreationDTO> {
    private readonly ITransactionService _transactionService;

    public CommandMessageConsumer(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task Consume(ConsumeContext<TransactionCreationDTO> context) 
    {        
        await _transactionService.InsertTransactionAsync(context.Message, context.CancellationToken);
    }
}