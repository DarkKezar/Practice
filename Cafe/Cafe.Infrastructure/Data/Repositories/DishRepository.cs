using Cafe.Domain.Entities;
using Cafe.Application.DTO;
using Cafe.Application.Interfaces;
using Cafe.Infrastructure.Data.DBContext;

namespace Cafe.Infrastructure.Data.Repositories;

public class DishRepository : BaseRepository<Dish>, IDishRepository
{
    public DishRepository(AppDbContext context)
    {
        _collection = context.GetDishCollection();
    }
}
