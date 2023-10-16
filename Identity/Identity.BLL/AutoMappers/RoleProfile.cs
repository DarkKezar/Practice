using AutoMapper;
using Identity.DAL.Models;
using Identity.BLL.DTO;

namespace Identity.BLL.AutoMappers;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<AppRole, GetAppRoleDTO>();
    }
}
