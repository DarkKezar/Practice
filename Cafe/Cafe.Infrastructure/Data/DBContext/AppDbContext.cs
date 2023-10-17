using Cafe.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
 
namespace Cafe.Infrastructure.Data.DBContext;

public class AppDbContext
{
    public readonly IMongoClient _mongoClient;
    public readonly CafeDatabaseSettings _settings;

    public AppDbContext(IOptions<CafeDatabaseSettings> settings, IMongoClient mongoClient)
    {
        _settings = settings.Value;
        _mongoClient = mongoClient;
    }

    public IMongoCollection<Bill> GetBillCollection()
    {
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Bill>(_settings.BillCollectionName);
    }

    public IMongoCollection<Dish> GetDishCollection()
    {
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Dish>(_settings.DishCollectionName);
    }

    public IMongoCollection<Employee> GetEmployeeCollection()
    {
        return _mongoClient.GetDatabase(_settings.DatabaseName)
                .GetCollection<Employee>(_settings.EmployeeCollectionName);
    }
}
