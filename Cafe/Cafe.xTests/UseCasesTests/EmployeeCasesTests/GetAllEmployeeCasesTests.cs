using AutoMapper;
using System.Net;
using Cafe.xTests.Moqs;
using Cafe.Application.Interfaces;
using Cafe.Application.Exceptions;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.EmployeeCases.Get;

namespace Cafe.xTests.UseCasesTests.EmployeeCasesTests;

public class GetAllEmployeeCasesTests
{
    public static IEmployeeRepository employeeRepository = RepositoriesMoq.GetIEmployeeRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static GetAllEmployeeQueryHandler handler = new GetAllEmployeeQueryHandler(mapper, employeeRepository);

    [Fact]
    public async void GetAllTest1()
    {
        var request = new GetAllEmployeeQuery(0, 10);

        var result = (OperationResult<List<GetEmployeeQueryResponse>>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
        Assert.Equal(result.ObjectResult.Count, DataMoq.Dishes.Count);
    }

    [Fact]
    public async void GetAllTest2()
    {
        var request = new GetAllEmployeeQuery(1, 10);

        var result = (OperationResult<List<GetEmployeeQueryResponse>>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
        Assert.Equal(result.ObjectResult.Count, 0);
    }

    [Fact]
    public async void GetAllTest3()
    {
        var request = new GetAllEmployeeQuery(-1, 10);

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
        Assert.True(((OperationWebException)ex).HttpStatusCode == (HttpStatusCode)400);
    }
}
