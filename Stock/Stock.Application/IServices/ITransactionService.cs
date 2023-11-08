using Stock.Application.DTO;
using Stock.Application.OperationResult;

namespace Stock.Application.IServices;

public interface ITransactionService
{
    Task<IOperationResult> InsertTransactionAsync(TransactionCreationDTO model, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetUserTransactionsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetTransactionAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetAllTransactionsAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default);
}