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

public class GetAllTests
{
    public static IIngridientRepository ingridientRepository = RepositoriesMoq.GetIngridientRepositry();
    public static ITransactionRepository transactionRepository = RepositoriesMoq.GetTransactionRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static IValidator<TransactionCreationDTO> validator = new TransactionCreationDTOValidator();
    public static ITransactionService service = new TransactionService(transactionRepository, ingridientRepository, mapper, validator);

    [Fact]
    public async Task GetAllTest1()
    {
        var result = (OperationResult<IList<Transaction>>)(await service.GetAllTransactionsAsync(0, 10));

        Assert.Equal(result.ObjectResult, DataMoq.Transactions);
    }

    [Fact]
    public async Task GetAllTest2()
    {
        var result = (OperationResult<IList<Transaction>>)(await service.GetAllTransactionsAsync(1, 10));

        Assert.Equal(result.ObjectResult, new List<Transaction>());
    }

    [Fact]
    public async Task GetAllTest3()
    {
        var e = new Exception();
        try
        {
            var result = (OperationResult<IList<Transaction>>)(await service.GetAllTransactionsAsync(-1, -10));
        }
        catch(Exception ex)
        {
            e = ex;
        }

        Assert.True(e is OperationWebException);
    }
}
