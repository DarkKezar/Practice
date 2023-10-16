using AutoMapper;
using Identity.DAL.Models;
using Identity.BLL.DTO;

namespace Identity.BLL.AutoMappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignUpModel, AppUser>().ReverseMap();
        CreateMap<AppUserUpdateModel, AppUser>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<AppUser, GetAppUserDTO>();
    }
}
