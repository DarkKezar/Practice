using AutoMapper;
using System.Net;
using Cafe.xTests.Moqs;
using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.DishCases.Create;

namespace Cafe.xTests.UseCasesTests.DishCasesTests;

public class CreateDishCasesTests
{
    public static IDishRepository dishRepository = RepositoriesMoq.GetIDishRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static CreateDishCommandHandler handler = new CreateDishCommandHandler(mapper, dishRepository);

    [Fact]
    public async void CreateTest1()
    {
        var request = new CreateDishCommand();
        var result = (OperationResult<Dish>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, HttpStatusCode.Created);
    }
}
