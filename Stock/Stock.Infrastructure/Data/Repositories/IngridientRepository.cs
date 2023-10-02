using Stock.Domain.Entities;
using Stock.Domain.Interfaces;
using Stock.Infrastructure.Data.DBContext;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Stock.Infrastructure.Data.Repositories;

public class IngridientRepository : IRepository<Ingridient>
{
    private readonly IMongoCollection<Ingridient> _ingridientCollection;

    public IngridientRepository(AppDbContext context)
    {
        _ingridientCollection = context.GetIngridientCollection();
    }
    
    public async Task<IQueryable<Ingridient>> GetAllAsync()
    {
        return _ingridientCollection.AsQueryable();
    }

    public async Task<Ingridient> GetByIdAsync(Guid id)
    {
        return await _ingridientCollection.FindAsync(new BsonDocument("_id", id));
    }

    public async Task DeleteAsync(Ingridient entity)
    {
        await _ingridientCollection.DeleteOneAsync(new BsonDocument("_id", entity.Id));
    }

    public async Task<Ingridient> UpdateAsync(Ingridient entity)
    {
        return await _ingridientCollection.FindOneAndReplaceAsync(new BsonDocument("_id", entity.Id), entity);
    }

    public async Task<Ingridient> CreateAsync(Ingridient entity)
    {
        await _ingridientCollection.InsertOneAsync(entity);

        return entity;
    }
}
