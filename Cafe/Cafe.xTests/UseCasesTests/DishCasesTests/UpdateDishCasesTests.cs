using AutoMapper;
using System.Net;
using Cafe.xTests.Moqs;
using Cafe.Application.Interfaces;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.DishCases.Update;

namespace Cafe.xTests.UseCasesTests.DishCasesTests;

public class UpdateDishCasesTests
{
    public static IDishRepository dishRepository = RepositoriesMoq.GetIDishRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static UpdateDishCommandHandler handler = new UpdateDishCommandHandler(mapper, dishRepository);

    [Fact]
    public async void UpdateTest1()
    {
        var request = new UpdateDishCommand()
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000000")
        };

        var result = (OperationResult<object>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
    }

    [Fact]
    public async void UpdateTest2()
    {
        var request = new UpdateDishCommand()
        {
            Id = Guid.Parse("20000000-0000-0000-0000-000000000000")
        };

        var result = (OperationResult<object>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
    }

    [Fact]
    public async void UpdateTest3()
    {
        var request = new UpdateDishCommand()
        {
            Id = Guid.Parse("33000000-0000-0000-0000-000000000000")
        };
        Exception ex = new Exception();
        try
        {
            var result = (OperationResult<object>)(await handler.Handle(request, default(CancellationToken)));
        }
        catch(Exception e)
        {
            ex = e;
        }

        Assert.True(ex is NullReferenceException);
    }
}
