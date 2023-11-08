using Cafe.Domain.Entities;

namespace Cafe.xTests.Moqs;

public static class DataMoq
{
    public static IList<Bill> Bills = new List<Bill>()
    {
        new Bill
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            DateTime = DateTime.Now,
            Sale = 0.00,
            Dishes = new List<Pair<Guid, double>>
            {
                new Pair<Guid, double>(Guid.Parse("10000000-0000-0000-0000-000000000000"), 1),
                new Pair<Guid, double>(Guid.Parse("20000000-0000-0000-0000-000000000000"), 1),
                new Pair<Guid, double>(Guid.Parse("30000000-0000-0000-0000-000000000000"), 1),
            }
        },
        new Bill
        {
            Id = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            DateTime = DateTime.Now,
            Sale = 0.45,
            Dishes = new List<Pair<Guid, double>>
            {
                new Pair<Guid, double>(Guid.Parse("10000000-0000-0000-0000-000000000000"), 2),
                new Pair<Guid, double>(Guid.Parse("20000000-0000-0000-0000-000000000000"), 2),
                new Pair<Guid, double>(Guid.Parse("30000000-0000-0000-0000-000000000000"), 2),
            }
        },
        new Bill
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000000"),
            DateTime = DateTime.Now,
            Sale = 0.10,
            Dishes = new List<Pair<Guid, double>>
            {
                new Pair<Guid, double>(Guid.Parse("10000000-0000-0000-0000-000000000000"), 3),
                new Pair<Guid, double>(Guid.Parse("20000000-0000-0000-0000-000000000000"), 3),
                new Pair<Guid, double>(Guid.Parse("30000000-0000-0000-0000-000000000000"), 3),
            }
        }
    };

    public static IList<Dish> Dishes = new List<Dish>()
    {
        new Dish
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            Name = "Dish 1",
            Price = 10,
            Description = "des dish 1",
            PhotosUrls = new List<string>(),
            Ingridients = new List<Pair<Guid, double>>
            {
                new Pair<Guid, double>(Guid.Parse("10000000-0000-0000-0000-000000000000"), 0.5),
                new Pair<Guid, double>(Guid.Parse("20000000-0000-0000-0000-000000000000"), 0.2),
                new Pair<Guid, double>(Guid.Parse("30000000-0000-0000-0000-000000000000"), 0.05),
            }
        },
        new Dish
        {
            Id = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            Name = "Dish 2",
            Price = 20,
            Description = "des dish 2",
            PhotosUrls = new List<string>(),
            Ingridients = new List<Pair<Guid, double>>
            {
                new Pair<Guid, double>(Guid.Parse("20000000-0000-0000-0000-000000000000"), 0.25),
                new Pair<Guid, double>(Guid.Parse("30000000-0000-0000-0000-000000000000"), 0.15),
            }
        },
        new Dish
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000000"),
            Name = "Dish 3",
            Price =  5,
            Description = "des dish 3",
            PhotosUrls = new List<string>(),
            Ingridients = new List<Pair<Guid, double>>
            {
                new Pair<Guid, double>(Guid.Parse("40000000-0000-0000-0000-000000000000"), 1.5),
            }
        }
    };

    public static IList<Employee> Employes = new List<Employee>()
    {
        new Employee
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            IdentityId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            Biography = "employee 1",
            Salary = 1000
        },
        new Employee
        {
            Id = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            IdentityId = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            Biography = "employee 2",
            Salary = 2000
        },
        new Employee
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000000"),
            IdentityId = Guid.Parse("30000000-0000-0000-0000-000000000000"),
            Biography = "employee 3",
            Salary = 3000
        }
    };
}
