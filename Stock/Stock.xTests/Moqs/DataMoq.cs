using Stock.Domain.Entities;

namespace Stock.xTests.Moqs;

public static class DataMoq
{
    public static List<Ingridient> Ingridients = new List<Ingridient>()
    {
        new Ingridient
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            Name = "NIngridient1",
            Supplies = 100
        },
        new Ingridient
        {
            Id = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            Name = "NIngridient2",
            Supplies =  10
        },
        new Ingridient
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000000"),
            Name = "NIngridient3",
            Supplies =  20
        },
        new Ingridient
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000000"),
            Name = "NIngridient4",
            Supplies = 200
        },
        new Ingridient
        {
            Id = Guid.Parse("50000000-0000-0000-0000-000000000000"),
            Name = "NIngridient5",
            Supplies = 123
        }
    };
    
    public static List<Transaction> Transactions = new List<Transaction>()
    {
        new Transaction
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            UserId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
                {
                    Guid.Parse("10000000-0000-0000-0000-000000000000"),
                    Guid.Parse("20000000-0000-0000-0000-000000000000"),
                    Guid.Parse("30000000-0000-0000-0000-000000000000")
                },
            Count = new List<double>()
                {
                    -10,
                    -20,
                    15
                },
            DateTime = DateTime.Now
        },
        new Transaction
        {
            Id = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            UserId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
                {
                    Guid.Parse("20000000-0000-0000-0000-000000000000"),
                    Guid.Parse("30000000-0000-0000-0000-000000000000"),
                    Guid.Parse("40000000-0000-0000-0000-000000000000")
                },
            Count = new List<double>()
                {
                    -10,
                    -20,
                    15
                },
            DateTime = DateTime.Now
        },
        new Transaction
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000000"),
            UserId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
                {
                    Guid.Parse("30000000-0000-0000-0000-000000000000"),
                    Guid.Parse("40000000-0000-0000-0000-000000000000"),
                    Guid.Parse("50000000-0000-0000-0000-000000000000")
                },
            Count = new List<double>()
                {
                    -10,
                    -20,
                    15
                },
            DateTime = DateTime.Now
        },
        new Transaction
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000000"),
            UserId = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
                {
                    Guid.Parse("40000000-0000-0000-0000-000000000000"),
                    Guid.Parse("50000000-0000-0000-0000-000000000000")
                },
            Count = new List<double>()
                {
                    -10,
                    -20,
                },
            DateTime = DateTime.Now
        },
        new Transaction
        {
            Id = Guid.Parse("50000000-0000-0000-0000-000000000000"),
            UserId = Guid.Parse("20000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
                {
                    Guid.Parse("10000000-0000-0000-0000-000000000000"),
                    Guid.Parse("20000000-0000-0000-0000-000000000000"),
                },
            Count = new List<double>()
                {
                    -20,
                    15
                },
            DateTime = DateTime.Now
        },
    };
}
