using Stock.Domain.Entities;
using Stock.Application.DTO;
using AutoMapper;

namespace Stock.Application.Automappers;

public class TransactionAutoMapper : Profile
{
    public TransactionAutoMapper()
    {
        CreateMap<TransactionCreationTO, Transaction>();
    }
}
