using System.Net;
using Core.Models;
using Core.Repositories.AppRoleRepository;
using Infr.CustomResult;

namespace Infr.Services.AppRoleService;

public class AppRoleService : IAppRoleService
{
    private readonly IAppRoleRepository appRoleRepository;

    public AppRoleService(IAppRoleRepository appRoleRepository){
        this.appRoleRepository = appRoleRepository;
    }

    public async Task<ApiResult> CreateRoleAsync(string Name)
    {
        AppRole Role = new AppRole(Name);
        HttpStatusCode httpStatusCode = HttpStatusCode.Created;
        string message = "Success";
        try{
            await appRoleRepository.CreateAppRoleAsync(Role);

        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
            Role = null;
        }

        return new ApiResult(message, httpStatusCode, Role);
    }

    public async Task<ApiResult> DeleteRoleAsync(string Name)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            var Role = await appRoleRepository.GetAppRoleAsync(Name);
            await appRoleRepository.DeleteAppRoleAsync(Role);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode);
    }

    public async Task<ApiResult> ReadRoleAsync(Guid Id)
    {
        AppRole Role = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            Role = await appRoleRepository.GetAppRoleAsync(Id);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode, Role);
    }

    public async Task<ApiResult> ReadRoleAsync(string Name)
    {
        AppRole Role = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            Role = await appRoleRepository.GetAppRoleAsync(Name);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode, Role);
    }

    public async Task<ApiResult> ReadRolesAsync(int page = 1, int count = 10)
    {
        List<AppRole> Roles = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            Roles = (await appRoleRepository.GetAllAppRoleAsync())
                    .Skip((page - 1) * count).Take(count).ToList();
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode, Roles);
    }
}
