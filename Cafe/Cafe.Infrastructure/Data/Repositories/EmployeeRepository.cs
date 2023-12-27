using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;
using Cafe.Infrastructure.Data.DBContext;

namespace Cafe.Infrastructure.Data.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context)
    {
        _collection = context.GetEmployeeCollection();
    }
}
