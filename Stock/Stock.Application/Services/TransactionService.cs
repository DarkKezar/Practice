using Stock.Domain.Entities;
using Stock.Application.IServices;
using Stock.Domain.Interfaces;

namespace Stock.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IRepository<Transaction> _repository;

    public TransactionService(IRepository<Transaction> repository){
        _repository = repository;
    }
    public async Task<Transaction> InsertTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
    public async Task<List<Transaction>> GetUserTransactionsAsync(Guid userId)
    {
        return (await _repository.GetAllAsync()).Where(t => t.UserId == userId).ToList();
    }
    public async Task<Transaction> GetTransactionAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
    public async Task<List<Transaction>> GetAllTransactionsAsync(int page = 0, int count = 10)
    {
        return (await _repository.GetAllAsync()).Skip(page * count).Take(count).ToList();
    }
}
