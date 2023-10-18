namespace Stock.Infrastructure.Data.DBContext;

public class StockDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string IngridientsCollectionName { get; set; } = null!;
    public string TransactionsCollectionName { get; set; } = null!;
}
