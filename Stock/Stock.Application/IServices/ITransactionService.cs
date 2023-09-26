using Stock.Domain.Entities;

namespace Stock.Application.IServices;

public interface ITransactionService
{
    Task<Transaction> InsertTransactionAsync(Transaction transaction);
    Task<Transaction> GetUserTransactionAsync(Guid userId);
    Task<Transaction> GetTransactionAsync(Guid id);
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
}
