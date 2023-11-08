using AutoMapper;
using Cafe.Application.Interfaces;
using Cafe.Application.Proto;
using Cafe.Application.Services;
using Cafe.Application.Validators;
using Cafe.xTests.Moqs;
using FluentValidation;
using Grpc.Core;

namespace Cafe.xTests.ServicesTests;

public class AccountCreationServiceTests
{
    public static IMapper mapper = MapperMoq.GetMapper();
    public static IEmployeeRepository employeeRepository = RepositoriesMoq.GetIEmployeeRepository();
    public static IValidator<AccountRequest> validator = new AccountRequestValidator();
    public static AccountCreationService service = new AccountCreationService(mapper, employeeRepository, validator);

    private const string success = "Succsess";
    private const string fail = "Fail";

    [Fact]
    public async Task CreationTest1()
    {
        var request = new AccountRequest()
        {
            IdentityIdString = "17000000-0000-0000-0000-000000000000",
            Biography = "BiographyBiography",
            Salary = 170
        };
        var result = await service.CreateAccount(request);

        Assert.Equal(result.Status, success);
    }

    [Fact]
    public async Task CreationTest2()
    {
        var request = new AccountRequest()
        {
            IdentityIdString = "17000000-0000-0000-0000-000000000000",
            Biography = "BiographyBiography",
            Salary = -170
        };
        var result = await service.CreateAccount(request);

        Assert.Equal(result.Status, fail);
    }
    [Fact]
    public async Task CreationTest3()
    {
        var request = new AccountRequest()
        {
            IdentityIdString = "identity id not guid",
            Biography = "BiographyBiography",
            Salary = 70
        };
        var result = await service.CreateAccount(request);

        Assert.Equal(result.Status, fail);
    }
}
