using Stock.Domain.Entities;
using Stock.Application.DTO;
using AutoMapper;

namespace Stock.Application.Automappers;

public class IngridientProfile : Profile
{
    public IngridientProfile()
    {
        CreateMap<IngridientCreationDTO, Ingridient>();
    }
}
