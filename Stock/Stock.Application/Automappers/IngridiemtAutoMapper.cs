using Stock.Domain.Entities;
using Stock.Application.DTO;
using AutoMapper;

namespace Stock.Application.Automappers;

public class IngridiemtAutoMapper : Profile
{
    public IngridiemtAutoMapper()
    {
        CreateMap<IngridientCreationTO, Ingridient>();
    }
}
