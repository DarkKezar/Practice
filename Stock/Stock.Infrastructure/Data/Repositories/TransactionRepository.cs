using Stock.Domain.Entities;
using Stock.Domain.Interfaces;
using Stock.Infrastructure.Data.DBContext;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Stock.Infrastructure.Data.Repositories;

public class TransactionRepository : IRepository<Transaction>
{
    private readonly IMongoCollection<Transaction> _transactionCollection;

    public TransactionRepository(AppDbContext context)
    {
        _transactionCollection = context.GetTransactionCollection();
    }
    
    public async Task<IQueryable<Transaction>> GetAllAsync()
    {
        return _transactionCollection.AsQueryable();
    }

    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        return await _transactionCollection.FindAsync(new BsonDocument("_id", id));
    }

    public async Task DeleteAsync(Transaction entity)
    {
        await _transactionCollection.DeleteOneAsync(new BsonDocument("_id", entity.Id));
    }

    public async Task<Transaction> UpdateAsync(Transaction entity)
    {
        return await _transactionCollection.FindOneAndReplaceAsync(new BsonDocument("_id", entity.Id), entity);
    }

    public async Task<Transaction> CreateAsync(Transaction entity)
    {
        await _transactionCollection.InsertOneAsync(entity);

        return entity;
    }
}
