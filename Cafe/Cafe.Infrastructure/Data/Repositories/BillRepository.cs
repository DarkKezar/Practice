using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;
using Cafe.Infrastructure.Data.DBContext;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Data.Repositories;

public class BillRepository : BaseRepository<Bill>, IBillRepository
{
    public BillRepository(AppDbContext context)
    {
        _collection = context.GetBillCollection();
    }

    public async Task<IList<Bill>> GetDailyBillsAsync()
    {
        return await _collection.AsQueryable().Where(b => b.DateTime.Date == DateTime.Now.Date).ToListAsync();
    }
}
