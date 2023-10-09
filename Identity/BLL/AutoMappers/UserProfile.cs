using AutoMapper;
using DAL.Models;
using BLL.DTO;

namespace BLL.AutoMappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignUpModel, AppUser>();
        CreateMap<AppUser, SignUpModel>();
    }
}
