using AutoMapper;
using Cafe.Domain.Entities;
using Cafe.Application.UseCases.EmployeeCases.Create;
using Cafe.Application.UseCases.EmployeeCases.Get;
using Cafe.Application.UseCases.EmployeeCases.Update;

namespace Cafe.Application.AutoMappers;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<CreateEmployeeCommand, Employee>();
        CreateMap<UpdateEmployeeCommand, Employee>();
        CreateMap<Employee, GetEmployeeQueryResponse>();
    }
}
