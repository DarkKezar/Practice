using AutoMapper;
using Identity.DAL.Models;
using Identity.BLL.DTO;

namespace Identity.BLL.AutoMappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignUpModel, AppUser>();
        CreateMap<AppUser, SignUpModel>();
    }
}
