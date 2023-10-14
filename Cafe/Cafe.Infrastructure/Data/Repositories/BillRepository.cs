using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;
using Cafe.Infrastructure.Data.DBContext;

namespace Cafe.Infrastructure.Data.Repositories;

public class BillRepository : BaseRepository<Bill>, IBillRepository
{
    public BillRepository(AppDbContext context)
    {
        _collection = context.GetBillCollection();
    }
}
