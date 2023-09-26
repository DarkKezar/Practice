using Stock.Domain.Entities;

namespace Stock.Domain.Interfaces;

public interface IStockAction
{
    Task UpdateIngridients(Transaction transaction);
}
