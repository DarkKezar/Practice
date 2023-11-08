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

namespace Stock.xTests.ServicesTests.IngridientServiceTests;

public class CreateTests
{
    public static IIngridientRepository ingridientRepository = RepositoriesMoq.GetIngridientRepositry();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static IValidator<IngridientCreationDTO> validator = new IngridientCreationDTOValidator();
    public static IIngridientService service = new IngridientService(ingridientRepository, mapper, validator);

    [Fact]
    public async Task CreationTest1()
    {
        var request = new IngridientCreationDTO
        {
            Name = "Ingridient"
        };
        var result = (OperationResult<Ingridient>)(await service.CreateIngridientAsync(request));

        Assert.Equal(result.HttpStatusCode, HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreationTest2()
    {
        var request = new IngridientCreationDTO
        {
            Name = ""
        };
        Exception e = new Exception();
        try
        {
            var result = (OperationResult<Ingridient>)(await service.CreateIngridientAsync(request));
        }
        catch(Exception ex)
        {
            e = ex;
        }

        Assert.True(e is OperationWebException);
    }
}
