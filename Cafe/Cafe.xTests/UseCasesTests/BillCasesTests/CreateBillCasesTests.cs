using AutoMapper;
using System.Net;
using MassTransit;
using Cafe.xTests.Moqs;
using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.BillCases.Create;

namespace Cafe.xTests.UseCasesTests.BillCasesTests;

public class CreateBillCasesTests
{
    public static IDishRepository dishRepository = RepositoriesMoq.GetIDishRepository();
    public static IBillRepository billRepository = RepositoriesMoq.GetIBillRepository();
    public static IPublishEndpoint publishEndPoint = PublishEndpointMoq.GetPublishEndpoint();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static CreateBillCommandHandler handler = new CreateBillCommandHandler(mapper, billRepository, publishEndPoint, dishRepository);

    [Fact]
    public async void CreateTest1()
    {
        var request = new CreateBillCommand();
        var result = (OperationResult<Bill>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, HttpStatusCode.Created);
    }
}
