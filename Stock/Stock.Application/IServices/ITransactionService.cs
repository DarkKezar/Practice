using Stock.Domain.Entities;

namespace Stock.Application.IServices;

public interface ITransactionService
{
    Task<Transaction> InsertTransactionAsync(Transaction transaction);
    Task<List<Transaction>> GetUserTransactionsAsync(Guid userId);
    Task<Transaction> GetTransactionAsync(Guid id);
    Task<List<Transaction>> GetAllTransactionsAsync(int page = 0, int count = 10);
}
