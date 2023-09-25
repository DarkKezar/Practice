using System.Net;
using DAL.Models;
using DAL.Repositories.AppRoleRepository;
using BLL.CustomResult;

namespace BLL.Services.AppRoleService;

public class AppRoleService : IAppRoleService
{
    private readonly IAppRoleRepository _appRoleRepository;

    public AppRoleService(IAppRoleRepository appRoleRepository)
    {
        _appRoleRepository = appRoleRepository;
    }

    public async Task<IApiResult> CreateRoleAsync(string name)
    {
        AppRole Role = new AppRole(name);
        HttpStatusCode httpStatusCode = HttpStatusCode.Created;
        string message = "Success";
        try{
            await _appRoleRepository.CreateAppRoleAsync(Role);

        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
            Role = null;
        }

        return new ApiObjectResult<AppRole>(message, httpStatusCode, Role);
    }

    public async Task<IApiResult> DeleteRoleAsync(string name)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            var Role = await _appRoleRepository.GetAppRoleAsync(name);
            await _appRoleRepository.DeleteAppRoleAsync(Role);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode);
    }

    public async Task<IApiResult> ReadRoleAsync(Guid id)
    {
        AppRole Role = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            Role = await _appRoleRepository.GetAppRoleAsync(id);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiObjectResult<AppRole>(message, httpStatusCode, Role);
    }

    public async Task<IApiResult> ReadRoleAsync(string name)
    {
        AppRole Role = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            Role = await _appRoleRepository.GetAppRoleAsync(name);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiObjectResult<AppRole>(message, httpStatusCode, Role);
    }

    public async Task<IApiResult> ReadRolesAsync(int page = 1, int count = 10)
    {
        List<AppRole> Roles = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try{
            Roles = (await _appRoleRepository.GetAllAppRoleAsync())
                    .Skip((page - 1) * count).Take(count).ToList();
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiObjectResult<List<AppRole>>(message, httpStatusCode, Roles);
    }
}
