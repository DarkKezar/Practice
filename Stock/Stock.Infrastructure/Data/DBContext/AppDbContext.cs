using Stock.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace Stock.Infrastructure.Data.DBContext;

public class AppDbContext
{
    public readonly MongoClient _mongoClient;
    public readonly IOptions<StockDatabaseSettings> _settings;

    public AppDbContext(IOptions<StockDatabaseSettings> settings)
    {
        _settings = settings;
        _mongoClient = new MongoClient(_settings.Value.ConnectionString);
    }

    public IMongoCollection<Ingridient> GetIngridientCollection()
    {
        return _mongoClient.GetDatabase(_settings.Value.DatabaseName)
                .GetCollection<Ingridient>(_settings.Value.IngridientsCollectionName);
    }

    public IMongoCollection<Transaction> GetTransactionCollection()
    {
        return _mongoClient.GetDatabase(_settings.Value.DatabaseName)
                .GetCollection<Transaction>(_settings.Value.TransactionsCollectionName);
    }
}
