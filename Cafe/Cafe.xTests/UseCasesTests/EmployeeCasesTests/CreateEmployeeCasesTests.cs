using AutoMapper;
using System.Net;
using Cafe.xTests.Moqs;
using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.EmployeeCases.Create;

namespace Cafe.xTests.UseCasesTests.EmployeeCasesTests;

public class CreateEmployeeCasesTests
{
    public static IEmployeeRepository employeeRepository = RepositoriesMoq.GetIEmployeeRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static CreateEmployeeCommandHandler handler = new CreateEmployeeCommandHandler(mapper, employeeRepository);

    [Fact]
    public async void CreateTest1()
    {
        var request = new CreateEmployeeCommand();
        var result = (OperationResult<Employee>)(await handler.Handle(request, default(CancellationToken)));

        Assert.Equal<HttpStatusCode>(result.HttpStatusCode, HttpStatusCode.Created);
    }
}
