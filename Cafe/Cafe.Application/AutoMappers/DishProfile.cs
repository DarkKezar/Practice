using AutoMapper;
using Cafe.Domain.Entities;
using Cafe.Application.UseCases.DishCases.Create;
using Cafe.Application.UseCases.DishCases.Get;
using Cafe.Application.UseCases.DishCases.Update;

namespace Cafe.Application.AutoMappers;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<CreateDishCommand, Dish>();
        CreateMap<UpdateDishCommand, Dish>();
        CreateMap<Dish, GetDishQueryResponse>();
    }
}
