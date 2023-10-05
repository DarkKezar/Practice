namespace Cafe.Infrastructure.Data.DBContext;

public class CafeDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string BillCollectionName { get; set; } = null!;
    public string DishCollectionName { get; set; } = null!;
    public string EmployeeCollectionName { get; set; } = null!;
}
