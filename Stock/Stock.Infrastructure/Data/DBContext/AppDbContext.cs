using Stock.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace Stock.Infrastructure.Data.DBContext;

public class AppDbContext
{
    public readonly IMongoClient _mongoClient;
    public readonly StockDatabaseSettings _settings;

    public AppDbContext(IOptions<StockDatabaseSettings> settings, IMongoClient mongoClient)
    {
        _settings = settings.Value;
        _mongoClient = mongoClient;
    }

    public IMongoCollection<Ingridient> GetIngridientCollection()
    {
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Ingridient>(_settings.IngridientsCollectionName);
    }

    public IMongoCollection<Transaction> GetTransactionCollection()
    {
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Transaction>(_settings.TransactionsCollectionName);
    }
}
