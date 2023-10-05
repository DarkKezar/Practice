using Cafe.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
 
namespace Cafe.Infrastructure.Data.DBContext;

public class AppDbContext
{
    public readonly MongoClient _mongoClient;
    public readonly CafeDatabaseSettings _settings;

    public AppDbContext(CafeDatabaseSettings settings){
        _settings = settings;
        _mongoClient = new MongoClient(_settings.ConnectionString);
    }

    public IMongoCollection<Bill> GetBillCollection(){
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Bill>(_settings.BillCollectionName);
    }

    public IMongoCollection<Dish> GetDishCollection(){
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Dish>(_settings.DishCollectionName);
    }

    public IMongoCollection<Employee> GetEmployeeCollection(){
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Employee>(_settings.EmployeeCollectionName);
    }
}
