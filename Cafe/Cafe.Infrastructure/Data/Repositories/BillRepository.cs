using Cafe.Domain.Entities;
using Cafe.Infrastructure.Data.DBContext;

namespace Cafe.Infrastructure.Data.Repositories;

public class BillRepository : BaseRepository<Bill>
{
    public BillRepository(AppDbContext context)
    {
        _collection = context.GetBillCollection();
    }
}
