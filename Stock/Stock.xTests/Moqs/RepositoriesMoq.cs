using Moq;
using Stock.Domain.Entities;
using Stock.Application.Interfaces;

namespace Stock.xTests.Moqs;

public static class RepositoriesMoq
{
    public static IIngridientRepository GetIngridientRepositry()
    {
        var result = new Mock<IIngridientRepository>();
        result.Setup(a => a.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns((int page, int count, CancellationToken token) => 
            {
                return Task.FromResult((IList<Ingridient>)DataMoq.Ingridients.Skip(page * count).Take(count).ToList());
            });
        result.Setup(a => a.GetIQueryable(It.IsAny<CancellationToken>()))
            .Returns((CancellationToken token) => {
                return DataMoq.Ingridients.AsQueryable();
            });
        result.Setup(a => a.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken token) => 
            {
                return Task.FromResult(DataMoq.Ingridients.FirstOrDefault(d => d.Id.Equals(id)));
            });
        result.Setup(a => a.DeleteAsync(It.IsAny<Ingridient>(), It.IsAny<CancellationToken>()))
            .Returns((Ingridient entity, CancellationToken token) =>
            {
                if(entity == null)
                    throw new Exception("Not found");
                return Task.FromResult(default(object));
            });
        result.Setup(a => a.UpdateAsync(It.IsAny<Ingridient>(), It.IsAny<CancellationToken>()))
            .Returns((Ingridient entity, CancellationToken token) =>
            {
                if(DataMoq.Ingridients.Where(d => d.Id != entity.Id).Count() == 0)
                    throw new Exception("Not found");
                return Task.FromResult(entity);
            });
        result.Setup(a => a.CreateAsync(It.IsAny<Ingridient>(), It.IsAny<CancellationToken>()))
            .Returns((Ingridient entity, CancellationToken token) =>
            {
                return Task.FromResult(entity);
            });

        return result.Object;
    }

    public static ITransactionRepository GetTransactionRepository()
    {
        var result = new Mock<ITransactionRepository>();
        result.Setup(a => a.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns((int page, int count, CancellationToken token) => 
            {
                return Task.FromResult((IList<Transaction>)DataMoq.Transactions.Skip(page * count).Take(count).ToList());
            });
        result.Setup(a => a.GetIQueryable(It.IsAny<CancellationToken>()))
            .Returns((CancellationToken token) => {
                return DataMoq.Transactions.AsQueryable();
            });
        result.Setup(a => a.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken token) => 
            {
                return Task.FromResult(DataMoq.Transactions.FirstOrDefault(d => d.Id.Equals(id)));
            });
        result.Setup(a => a.DeleteAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
            .Returns((Transaction entity, CancellationToken token) =>
            {
                if(entity == null)
                    throw new Exception("Not found");
                return Task.FromResult(default(object));
            });
        result.Setup(a => a.UpdateAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
            .Returns((Transaction entity, CancellationToken token) =>
            {
                if(DataMoq.Transactions.Where(d => d.Id != entity.Id).Count() == 0)
                    throw new Exception("Not found");
                return Task.FromResult(entity);
            });
        result.Setup(a => a.CreateAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
            .Returns((Transaction entity, CancellationToken token) =>
            {
                return Task.FromResult(entity);
            });

        return result.Object;
    }
}
