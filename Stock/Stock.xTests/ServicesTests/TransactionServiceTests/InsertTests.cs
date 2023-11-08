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

public class InsertTests
{
    public static IIngridientRepository ingridientRepository = RepositoriesMoq.GetIngridientRepositry();
    public static ITransactionRepository transactionRepository = RepositoriesMoq.GetTransactionRepository();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static IValidator<TransactionCreationDTO> validator = new TransactionCreationDTOValidator();
    public static ITransactionService service = new TransactionService(transactionRepository, ingridientRepository, mapper, validator);

    [Fact]
    public async Task InsertTest1()
    {
        var request = new TransactionCreationDTO
        {
            UserId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
            {
                Guid.Parse("10000000-0000-0000-0000-000000000000")
            },
            Count = new List<double>()
            {
                10
            }
        };
        var result = (OperationResult<Transaction>)(await service.InsertTransactionAsync(request));

        Assert.Equal(result.HttpStatusCode, HttpStatusCode.Created);
    }

    [Fact]
    public async Task InsertTest2()
    {
        var request = new TransactionCreationDTO
        {
            UserId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
            {
                Guid.Parse("10000000-0000-0000-0000-000000000000")
            },
            Count = new List<double>()
            {
                10, 20, 30
            }
        };
        Exception e = new Exception();
        try
        {
            var result = (OperationResult<Ingridient>)(await service.InsertTransactionAsync(request));
        }
        catch(Exception ex)
        {
            e = ex;
        }

        Assert.True(e is OperationWebException);
    }

    [Fact]
    public async Task InsertTest3()
    {
        var request = new TransactionCreationDTO
        {
            UserId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            IngridientsId = new List<Guid>()
            { },
            Count = new List<double>()
            { }
        };
        Exception e = new Exception();
        try
        {
            var result = (OperationResult<Ingridient>)(await service.InsertTransactionAsync(request));
        }
        catch(Exception ex)
        {
            e = ex;
        }

        Assert.True(e is OperationWebException);
    }
}
