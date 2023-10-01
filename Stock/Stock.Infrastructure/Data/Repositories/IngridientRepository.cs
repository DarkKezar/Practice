using Stock.Domain.Entities;
using Stock.Domain.Interfaces;
using Stock.Infrastructure.Data.DBContext;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Stock.Infrastructure.Data.Repositories;

public class IngridientRepository : IRepository<Ingridient>
{
    private readonly IMongoCollection<Ingridient> _collection;

    public IngridientRepository(AppDbContext context){
        _collection = context.GetIngridientCollection();
    }
    public async Task<IQueryable<Ingridient>> GetAllAsync()
    {
        return _collection.AsQueryable();
    }
    public async Task<Ingridient> GetByIdAsync(Guid id)
    {
        //I dont know, why it didn't work with SingleOrDefaultAsync()

        return  (_collection.AsQueryable()).SingleOrDefault(o => o.Id == id);
    }
    public async Task DeleteAsync(Ingridient entity)
    {
        await _collection.DeleteOneAsync(new BsonDocument("_id", entity.Id));
    }
    public async Task<Ingridient> UpdateAsync(Ingridient entity)
    {
        return await _collection.FindOneAndReplaceAsync(
                        new BsonDocument("_id", entity.Id), entity
                    );
    }
    public async Task<Ingridient> CreateAsync(Ingridient entity)
    {
        await _collection.InsertOneAsync(entity);

        return entity;
    }
}
