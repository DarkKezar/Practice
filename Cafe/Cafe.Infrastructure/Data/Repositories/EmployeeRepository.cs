using Cafe.Domain.Entities;
using Cafe.Infrastructure.Data.DBContext;

namespace Cafe.Infrastructure.Data.Repositories;

public class EmployeeRepository : BaseRepository<Employee>
{
    public EmployeeRepository(AppDbContext context)
    {
        _collection = context.GetEmployeeCollection();
    }
}
