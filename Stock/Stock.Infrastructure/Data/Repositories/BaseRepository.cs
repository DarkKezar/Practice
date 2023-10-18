using Stock.Domain.Entities;
using Stock.Application.Interfaces;
using Stock.Infrastructure.Data.DBContext;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Stock.Infrastructure.Data.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected IMongoCollection<T> _collection;

    public async Task<IList<T>> GetAllAsync(int page, int count, CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable().Skip(page * count).Take(count).ToListAsync();
    }

    public IQueryable<T> GetIQueryable(CancellationToken cancellationToken = default)
    {
        return _collection.AsQueryable();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var r = await (await _collection.FindAsync(new BsonDocument("_id", id))).ToListAsync();
        
        return (r.Count == 0) ? null : r[0];
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(new BsonDocument("_id", entity.Id));
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = await _collection.FindOneAndReplaceAsync(new BsonDocument("_id", entity.Id), entity);

        return result;
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity);

        return entity;
    }
}
