using Stock.Domain.Entities;

namespace Stock.Application.IServices;

public interface IIngridientService
{
    Task<IEnumerable<Ingridient>> GetAllIngridientAsync();
    Task<Ingridient> GetIngridientAsync();
}
