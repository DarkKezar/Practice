using AutoMapper;
using Cafe.Application.AutoMappers;

namespace Cafe.xTests.Moqs;

public static class MapperMoq
{
    public static IMapper GetMapper()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BillProfile());
            cfg.AddProfile(new DishProfile());
            cfg.AddProfile(new EmployeeProfile());
        });
        var result = mockMapper.CreateMapper();

        return result;
    }
}
