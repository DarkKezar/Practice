using Stock.Application.Automappers;
using AutoMapper;

namespace Stock.xTests.Moqs;

public static class MapperMoq
{
    public static IMapper GetMapper()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new IngridientProfile());
            cfg.AddProfile(new TransactionProfile());
        });
        var result = mockMapper.CreateMapper();

        return result;
    }
}
