using AutoMapper;
using Cafe.Domain.Entities;
using Cafe.Application.UseCases.BillCases.Get;
using Cafe.Application.UseCases.BillCases.Create;

namespace Cafe.Application.AutoMappers;

public class BillProfile : Profile
{
    public BillProfile()
    {
        CreateMap<CreateBillCommand, Bill>();
        
        CreateMap<Bill, GetBillQueryResponse>();
    }
}
