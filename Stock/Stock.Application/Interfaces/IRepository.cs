using Stock.Domain.Entities;

namespace Stock.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IQueryable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task DeleteAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> CreateAsync(T entity);
}
