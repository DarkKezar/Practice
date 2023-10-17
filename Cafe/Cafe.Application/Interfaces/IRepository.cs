using Cafe.Domain.Entities;

namespace Cafe.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IList<T>> GetAllAsync(int page, int count, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
}
