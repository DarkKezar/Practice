using Stock.Domain.Entities;
using Stock.Application.IServices;
using Stock.Domain.Interfaces;

namespace Stock.Application.Services;

public class IngridientService : IIngridientService
{
    private readonly IRepository<Ingridient> _ingridientRepository;

    public IngridientService(IRepository<Ingridient> repository)
    {
        _ingridientRepository = repository;
    }

    public async Task<Ingridient> CreateIngridientAsync(Ingridient ingridient)
    {
        return await _ingridientRepository.CreateAsync(ingridient);
    }

    public async Task<List<Ingridient>> GetAllIngridientAsync(int page = 0, int count = 10)
    {
        return (await _ingridientRepository.GetAllAsync()).Skip(page * count).Take(count).ToList();
    }

    public async Task<Ingridient> GetIngridientAsync(Guid id)
    {
        return await _ingridientRepository.GetByIdAsync(id);
    }
}
