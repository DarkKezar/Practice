using Stock.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Stock.Infrastructure.Data.DBContext;

public class AppDbContext
{
    //This class should be used as Singleton
    public readonly MongoClient _mongoClient;
    public readonly StockDatabaseSettings _settings;

    public AppDbContext(StockDatabaseSettings settings){
        _settings = settings;
        _mongoClient = new MongoClient(_settings.ConnectionString);
    }

    public IMongoCollection<Ingridient> GetIngridientCollection(){
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Ingridient>(_settings.IngridientsCollectionName);
    }

    public IMongoCollection<Transaction> GetTransactionCollection(){
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Transaction>(_settings.TransactionsCollectionName);
    }
}
