using Stock.Domain.Entities;
using Stock.Domain.Interfaces;
using Stock.Infrastructure.Data.DBContext;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Stock.Infrastructure.Data.Repositories;

public class TransactionRepository : IRepository<Transaction>
{
    private readonly IMongoCollection<Transaction> _collection;

    public TransactionRepository(AppDbContext context){
        _collection = context.GetTransactionCollection();
    }
    public async Task<IQueryable<Transaction>> GetAllAsync()
    {
        return _collection.AsQueryable();
    }
    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        //I dont know, why it didn't work with SingleOrDefaultAsync()

        return  (_collection.AsQueryable()).SingleOrDefault(o => o.Id == id);
    }
    public async Task DeleteAsync(Transaction entity)
    {
        await _collection.DeleteOneAsync(new BsonDocument("_id", entity.Id));
    }
    public async Task<Transaction> UpdateAsync(Transaction entity)
    {
        return await _collection.FindOneAndReplaceAsync(
                        new BsonDocument("_id", entity.Id), entity
                    );
    }
    public async Task<Transaction> CreateAsync(Transaction entity)
    {
        await _collection.InsertOneAsync(entity);

        return entity;
    }
}
