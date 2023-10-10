using Stock.Domain.Entities;
using Stock.Application.DTO;
using Stock.Application.DTO.ApiResult;

namespace Stock.Application.IServices;

public interface ITransactionService
{
    Task<IApiResult> InsertTransactionAsync(TransactionCreationDTO model, CancellationToken cancellationToken = default);
    Task<IApiResult> GetUserTransactionsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IApiResult> GetTransactionAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IApiResult> GetAllTransactionsAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default);
}
