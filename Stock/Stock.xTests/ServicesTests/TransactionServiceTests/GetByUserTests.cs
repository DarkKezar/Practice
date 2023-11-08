using AutoMapper;
using System.Net;
using FluentValidation;
using Stock.Domain.Entities;
using Stock.xTests.Moqs;
using Stock.Application.Interfaces;
using Stock.Application.Exceptions;
using Stock.Application.OperationResult;
using Stock.Application.IServices;
using Stock.Application.Validators;
using Stock.Application.Services;
using Stock.Application.DTO;

namespace Stock.xTests.ServicesTests.TransactionServiceTests;

public class GetByUserTests
{
    public static IIngridientRepository ingridientRepository = RepositoriesMoq.GetIngridientRepositry();
    public static ITransactionRepository transactionRepository = RepositoriesMoq.GetTransactionRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static IValidator<TransactionCreationDTO> validator = new TransactionCreationDTOValidator();
    public static ITransactionService service = new TransactionService(transactionRepository, ingridientRepository, mapper, validator);

    [Fact]
    public async Task GetByUserTest1()
    {
        var request = Guid.Parse("10000000-0000-0000-0000-000000000000");
        var result = (OperationResult<IList<Transaction>>)(await service.GetUserTransactionsAsync(request));

        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
        Assert.Equal(result.ObjectResult.Count, 3);
    }

    [Fact]
    public async Task GetByUserTest2()
    {
        var request = Guid.Parse("20000000-0000-0000-0000-000000000000");
        var result = (OperationResult<IList<Transaction>>)(await service.GetUserTransactionsAsync(request));

        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
        Assert.Equal(result.ObjectResult.Count, 2);
    }

    [Fact]
    public async Task GetByUserTest3()
    {
        var request = Guid.Parse("30000000-0000-0000-0000-000000000000");
        var result = (OperationResult<IList<Transaction>>)(await service.GetUserTransactionsAsync(request));

        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
        Assert.Equal(result.ObjectResult.Count, 0);
    }
}
