using Stock.Domain.Entities;
using Stock.Application.IServices;

namespace Stock.Application.Services;

public class TransactionService : ITransactionService
{
    public async Task<Transaction> InsertTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
    public async Task<Transaction> GetUserTransactionAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
    public async Task<Transaction> GetTransactionAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        throw new NotImplementedException();
    }
}
