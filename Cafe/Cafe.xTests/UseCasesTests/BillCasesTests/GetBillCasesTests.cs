using AutoMapper;
using System.Net;
using Cafe.xTests.Moqs;
using Cafe.Application.Interfaces;
using Cafe.Application.Exceptions;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.BillCases.Get;

namespace Cafe.xTests.UseCasesTests.BillCasesTests;

public class GetBillCasesTests
{
    public static IBillRepository billRepository = RepositoriesMoq.GetIBillRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static GetBillQueryHandler handler = new GetBillQueryHandler(mapper, billRepository);

    [Fact]
    public async void GetTest1()
    {
        var request = new GetBillQuery(Guid.Parse("10000000-0000-0000-0000-000000000000"));

        var result = (OperationResult<GetBillQueryResponse>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
        Assert.Equal(result.ObjectResult.Id, DataMoq.Bills[0].Id);
    }

    [Fact]
    public async void GetTest2()
    {
        var request = new GetBillQuery(Guid.Parse("20000000-0000-0000-0000-000000000000"));

        var result = (OperationResult<GetBillQueryResponse>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
        Assert.Equal(result.ObjectResult.Id, DataMoq.Bills[1].Id);
    }

    [Fact]
    public async void GetTest3()
    {
        var request = new GetBillQuery(Guid.Parse("17000000-0000-0000-0000-000000000000"));

        Exception ex = new Exception();
        try
        {
            var result = await handler.Handle(request, default(CancellationToken));
            
        }
        catch (Exception e)
        {
            ex = e;
        }

        Assert.True(ex is OperationWebException);
        Assert.True(((OperationWebException)ex).HttpStatusCode == (HttpStatusCode)404);
    }
}
