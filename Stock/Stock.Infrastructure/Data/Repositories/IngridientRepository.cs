using Stock.Domain.Entities;
using Stock.Domain.Interfaces;

namespace Stock.Infrastructure.Data.Repositories;

public class IngridientRepository : IRepository<Ingridient>
{
    public async Task<IQueryable<Ingridient>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<Ingridient> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task DeleteAsync(Ingridient entity)
    {
        throw new NotImplementedException();
    }
    public async Task<Ingridient> UpdateAsync(Ingridient entity)
    {
        throw new NotImplementedException();
    }
    public async Task<Ingridient> CreateAsync(Ingridient entity)
    {
        throw new NotImplementedException();
    }
}
