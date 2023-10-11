using Stock.Domain.Entities;
using Stock.Application.Interfaces;
using Stock.Infrastructure.Data.DBContext;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Stock.Infrastructure.Data.Repositories;

public class IngridientRepository : BaseRepository<Ingridient>, IIngridientRepository
{

    public IngridientRepository(AppDbContext context)
    {
        _collection = context.GetIngridientCollection();
    }
}
