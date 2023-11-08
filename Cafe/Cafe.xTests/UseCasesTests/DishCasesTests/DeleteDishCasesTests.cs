using System.Net;
using Cafe.xTests.Moqs;
using Cafe.Application.Interfaces;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.DishCases.Delete;

namespace Cafe.xTests.UseCasesTests.DishCasesTests;

public class DeleteDishCasesTests
{
    public static IDishRepository dishRepository = RepositoriesMoq.GetIDishRepository();
    public static DeleteDishCommandHandler handler = new DeleteDishCommandHandler(dishRepository);

    [Fact]
    public async void DeleteTest1()
    {
        var request = new DeleteDishCommand(Guid.Parse("10000000-0000-0000-0000-000000000000"));
        var result = (OperationResult<object>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, HttpStatusCode.OK);
    }

    [Fact]
    public async void DeleteTest2()
    {
        var request = new DeleteDishCommand(Guid.Parse("20000000-0000-0000-0000-000000000000"));
        var result = (OperationResult<object>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, HttpStatusCode.OK);
    }

    [Fact]
    public async void DeleteTest3()
    {
        var request = new DeleteDishCommand(Guid.Parse("33000000-0000-0000-0000-000000000000"));
        
        Exception ex = new Exception();
        try
        {
            var result = (OperationResult<object>)(await handler.Handle(request, default(CancellationToken)));
        }
        catch(Exception e)
        {
            ex = e;
        }

        Assert.True(ex.Message.Equals("Not found"));
    }
}
