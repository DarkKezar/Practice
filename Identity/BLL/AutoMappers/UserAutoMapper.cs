using AutoMapper;
using DAL.Models;
using BLL.DTO;

namespace BLL.AutoMappers;

public class UserAutoMapper : Profile
{
    public UserAutoMapper()
    {
        CreateMap<SignUpModel, AppUser>();
        CreateMap<AppUser, SignUpModel>();
    }
}
