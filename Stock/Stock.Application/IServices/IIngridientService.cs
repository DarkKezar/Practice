using Stock.Domain.Entities;

namespace Stock.Application.IServices;

public interface IIngridientService
{
    Task<Ingridient> CreateIngridientAsync(Ingridient ingridient);
    Task<List<Ingridient>> GetAllIngridientAsync(int page = 0, int count = 10);
    Task<Ingridient> GetIngridientAsync(Guid id);
}
