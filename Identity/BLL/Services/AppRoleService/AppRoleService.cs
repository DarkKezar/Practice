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
        AppRole role = new AppRole(name);
        HttpStatusCode httpStatusCode = HttpStatusCode.Created;
        string message = "Success";
        try
        {
            await _appRoleRepository.CreateAppRoleAsync(role);

        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
            role = null;
        }

        return new OperationResult<AppRole>(message, httpStatusCode, role);
    }

    public async Task<IApiResult> DeleteRoleAsync(string name)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try
        {
            AppRole role = await _appRoleRepository.GetAppRoleAsync(name);
            await _appRoleRepository.DeleteAppRoleAsync(role);
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<Object>(message, httpStatusCode);
    }

    public async Task<IApiResult> GetRoleAsync(Guid id)
    {
        AppRole role = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try
        {
            role = await _appRoleRepository.GetAppRoleAsync(id);
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<AppRole>(message, httpStatusCode, role);
    }

    public async Task<IApiResult> GetRoleAsync(string name)
    {
        AppRole role = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try
        {
            role = await _appRoleRepository.GetAppRoleAsync(name);
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<AppRole>(message, httpStatusCode, role);
    }

    public async Task<IApiResult> GetRolesAsync(int page = 1, int count = 10)
    {
        List<AppRole> roles = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.Accepted;
        string message = "Success";

        try
        {
            roles = (await _appRoleRepository.GetAllAppRoleAsync())
                    .Skip((page - 1) * count).Take(count).ToList();
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<List<AppRole>>(message, httpStatusCode, roles);
    }
}
