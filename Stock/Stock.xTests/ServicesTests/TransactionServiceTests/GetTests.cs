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

public class GetTests
{
    public static IIngridientRepository ingridientRepository = RepositoriesMoq.GetIngridientRepositry();
    public static ITransactionRepository transactionRepository = RepositoriesMoq.GetTransactionRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static IValidator<TransactionCreationDTO> validator = new TransactionCreationDTOValidator();
    public static ITransactionService service = new TransactionService(transactionRepository, ingridientRepository, mapper, validator);

    [Fact]
    public async Task GetTest1()
    {
        var request = Guid.Parse("10000000-0000-0000-0000-000000000000");
        var result = (OperationResult<Transaction>)(await service.GetTransactionAsync(request));

        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
        Assert.Equal(result.ObjectResult, DataMoq.Transactions[0]);
    }

    [Fact]
    public async Task GetTest2()
    {
        var request = Guid.Parse("70000000-0000-0000-0000-000000000000");
        var e = new Exception();
        try
        {
            var result = (OperationResult<Transaction>)(await service.GetTransactionAsync(request));
        }
        catch(Exception ex)
        {
            e = ex;
        }

        Assert.True(e is OperationWebException);
        Assert.Equal(((OperationWebException)e).HttpStatusCode, (HttpStatusCode)404);
    }
}
