using Stock.Domain.Entities;
using Stock.Domain.Interfaces;

namespace Stock.Infrastructure.Data.Repositories;

public class TransactionRepository : IRepository<Transaction>
{
    public async Task<IQueryable<Transaction>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task DeleteAsync(Transaction entity)
    {
        throw new NotImplementedException();
    }
    public async Task<Transaction> UpdateAsync(Transaction entity)
    {
        throw new NotImplementedException();
    }
    public async Task<Transaction> CreateAsync(Transaction entity)
    {
        throw new NotImplementedException();
    }
}
