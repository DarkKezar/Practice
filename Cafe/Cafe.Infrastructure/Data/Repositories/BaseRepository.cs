using Cafe.Application.Interfaces;
using Cafe.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Cafe.Infrastructure.Data.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected IMongoCollection<T> _collection;

    public async Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _collection.AsQueryable();
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var r = (await _collection.FindAsync(new BsonDocument("_id", id))).ToList();

        if(r != null)
            return r[0];
        else
            return null;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(new BsonDocument("_id", entity.Id));
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        return await _collection.FindOneAndReplaceAsync(new BsonDocument("_id", entity.Id), entity);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity);

        return entity;
    }
}