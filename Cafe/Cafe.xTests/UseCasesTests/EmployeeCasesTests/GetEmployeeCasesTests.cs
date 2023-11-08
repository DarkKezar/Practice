using AutoMapper;
using System.Net;
using Cafe.xTests.Moqs;
using Cafe.Application.Interfaces;
using Cafe.Application.Exceptions;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.EmployeeCases.Get;

namespace Cafe.xTests.UseCasesTests.EmployeeCasesTests;

public class GetEmployeeCasesTests
{
    public static IEmployeeRepository employeeRepository = RepositoriesMoq.GetIEmployeeRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static GetEmployeeQueryHandler handler = new GetEmployeeQueryHandler(mapper, employeeRepository);

    [Fact]
    public async void GetTest1()
    {
        var request = new GetEmployeeQuery(Guid.Parse("10000000-0000-0000-0000-000000000000"));

        var result = (OperationResult<GetEmployeeQueryResponse>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
        Assert.Equal(result.ObjectResult.IdentityId, DataMoq.Employes[0].IdentityId);
    }

    [Fact]
    public async void GetTest2()
    {
        var request = new GetEmployeeQuery(Guid.Parse("20000000-0000-0000-0000-000000000000"));

        var result = (OperationResult<GetEmployeeQueryResponse>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, (HttpStatusCode)200);
        Assert.Equal(result.ObjectResult.IdentityId, DataMoq.Employes[1].IdentityId);
    }

    [Fact]
    public async void GetTest3()
    {
        var request = new GetEmployeeQuery(Guid.Parse("17000000-0000-0000-0000-000000000000"));

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
