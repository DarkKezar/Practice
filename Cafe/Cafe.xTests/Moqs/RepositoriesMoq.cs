using Moq;
using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;

namespace Cafe.xTests.Moqs;

public static class RepositoriesMoq
{
    public static IBillRepository GetIBillRepository()
    {
        var result = new Mock<IBillRepository>();
        result.Setup(a => a.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns((int page, int count, CancellationToken token) => 
            {
                return Task.FromResult((IList<Bill>)DataMoq.Bills.Skip(page * count).Take(count).ToList());
            });
        result.Setup(a => a.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken token) => 
            {
                return Task.FromResult(DataMoq.Bills.FirstOrDefault(d => d.Id.Equals(id)));
            });
        result.Setup(a => a.DeleteAsync(It.IsAny<Bill>(), It.IsAny<CancellationToken>()))
            .Returns((Bill entity, CancellationToken token) =>
            {
                if(entity == null)
                    throw new Exception("Not found");
                return Task.FromResult(default(object));
            });
        result.Setup(a => a.UpdateAsync(It.IsAny<Bill>(), It.IsAny<CancellationToken>()))
            .Returns((Bill entity, CancellationToken token) =>
            {
                if(DataMoq.Bills.Where(d => d.Id != entity.Id).Count() == 0)
                    throw new Exception("Not found");
                return Task.FromResult(entity);
            });
        result.Setup(a => a.CreateAsync(It.IsAny<Bill>(), It.IsAny<CancellationToken>()))
            .Returns((Bill entity, CancellationToken token) =>
            {
                return Task.FromResult(entity);
            });
        result.Setup(a => a.GetDailyBills())
            .Returns(() => 
            {
                return (IList<Bill>)DataMoq.Bills;
            });

        return result.Object;
    }

    public static IDishRepository GetIDishRepository()
    {
        var result = new Mock<IDishRepository>();
        result.Setup(a => a.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns((int page, int count, CancellationToken token) => 
            {
                return Task.FromResult((IList<Dish>)DataMoq.Dishes.Skip(page * count).Take(count).ToList());
            });
        result.Setup(a => a.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken token) => 
            {
                return Task.FromResult(DataMoq.Dishes.FirstOrDefault(d => d.Id.Equals(id)));
            });
        result.Setup(a => a.DeleteAsync(It.IsAny<Dish>(), It.IsAny<CancellationToken>()))
            .Returns((Dish entity, CancellationToken token) =>
            {
                if(entity == null)
                    throw new Exception("Not found");
                return Task.FromResult(default(object));
            });
        result.Setup(a => a.UpdateAsync(It.IsAny<Dish>(), It.IsAny<CancellationToken>()))
            .Returns((Dish entity, CancellationToken token) =>
            {
                if(DataMoq.Dishes.Where(d => d.Id != entity.Id).Count() == 0)
                    throw new Exception("Not found");
                return Task.FromResult(entity);
            });
        result.Setup(a => a.CreateAsync(It.IsAny<Dish>(), It.IsAny<CancellationToken>()))
            .Returns((Dish entity, CancellationToken token) =>
            {
                return Task.FromResult(entity);
            });


        return result.Object;
    }

    public static IEmployeeRepository GetIEmployeeRepository()
    {
        var result = new Mock<IEmployeeRepository>();
        result.Setup(a => a.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns((int page, int count, CancellationToken token) => 
            {
                return Task.FromResult((IList<Employee>)DataMoq.Employes.Skip(page * count).Take(count).ToList());
            });
        result.Setup(a => a.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken token) => 
            {
                return Task.FromResult(DataMoq.Employes.FirstOrDefault(d => d.Id.Equals(id)));
            });
        result.Setup(a => a.DeleteAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
            .Returns((Employee entity, CancellationToken token) =>
            {
                if(entity == null)
                    throw new Exception("Not found");
                return Task.FromResult(default(object));
            });
        result.Setup(a => a.UpdateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
            .Returns((Employee entity, CancellationToken token) =>
            {
                if(DataMoq.Employes.Where(d => d.Id != entity.Id).Count() == 0)
                    throw new Exception("Not found");
                return Task.FromResult(entity);
            });
        result.Setup(a => a.CreateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
            .Returns((Employee entity, CancellationToken token) =>
            {
                return Task.FromResult(entity);
            });
        return result.Object;
    }

    public static IReportRepository GetIReportRepository()
    {
        var result = new Mock<IReportRepository>();
        result.Setup(a => a.RecordFileAsync(It.IsAny<string>(), It.IsAny<Byte[]>(), It.IsAny<CancellationToken>()))
            .Returns((string name, Byte[] bytes, CancellationToken cancellationToken) => {
                return Task.FromResult(default(object));
            });

        return result.Object;
    }
}
