using Cafe.Domain.Entities;
using Cafe.Infrastructure.Data.DBContext;

namespace Cafe.Infrastructure.Data.Repositories;

public class DishRepository : BaseRepository<Dish>
{
    public DishRepository(AppDbContext context)
    {
        _collection = context.GetDishCollection();
    }
}
