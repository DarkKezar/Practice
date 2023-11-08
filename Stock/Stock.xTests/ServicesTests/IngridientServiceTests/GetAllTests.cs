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

public class GetAllTests
{
    public static IIngridientRepository ingridientRepository = RepositoriesMoq.GetIngridientRepositry();
    public static IMapper mapper = MapperMoq.GetMapper();
    public static IValidator<IngridientCreationDTO> validator = new IngridientCreationDTOValidator();
    public static IIngridientService service = new IngridientService(ingridientRepository, mapper, validator);

    [Fact]
    public async Task GetAllTest1()
    {
        var result = (OperationResult<IList<Ingridient>>)(await service.GetAllIngridientAsync(0, 10));

        Assert.Equal(result.ObjectResult, DataMoq.Ingridients);
    }

    [Fact]
    public async Task GetAllTest2()
    {
        var result = (OperationResult<IList<Ingridient>>)(await service.GetAllIngridientAsync(1, 10));

        Assert.Equal(result.ObjectResult, new List<Ingridient>());
    }

    [Fact]
    public async Task GetAllTest3()
    {
        var e = new Exception();
        try
        {
            var result = (OperationResult<IList<Ingridient>>)(await service.GetAllIngridientAsync(-1, -10));
        }
        catch(Exception ex)
        {
            e = ex;
        }

        Assert.True(e is OperationWebException);
    }
}
