using Stock.Domain.Entities;
using Stock.Application.DTO;
using AutoMapper;

namespace Stock.Application.Automappers;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionCreationDTO, Transaction>();
    }
}