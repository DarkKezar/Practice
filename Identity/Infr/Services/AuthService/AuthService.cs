using Core.DTO;
using Core.Models;
using Core.Repositories.AppUserRepository;

namespace Infr.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IAppUserRepository appUserRepository;

    public AuthService(IAppUserRepository appUserRepository){
        this.appUserRepository = appUserRepository;
    }

    public async Task<AppUser> AuthAccountAsync(AuthModel model)
    {
        return await appUserRepository.AuthAppUserAsync(model);
    }
}
