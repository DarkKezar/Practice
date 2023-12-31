using Stock.Domain.Entities;

namespace Stock.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IList<T>> GetAllAsync(int page, int count, CancellationToken cancellationToken = default);
    IQueryable<T> GetIQueryable(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
}