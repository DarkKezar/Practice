using AutoMapper;
using Identity.DAL.Models;
using Identity.BLL.DTO;
using Identity.DLL.Proto;

namespace Identity.BLL.AutoMappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignUpModel, AppUser>().ReverseMap();
        CreateMap<SignUpModel, AccountRequest>();
        CreateMap<AppUserUpdateModel, AppUser>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<AppUser, GetAppUserDTO>();
    }
}
