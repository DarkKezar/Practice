using System.Net;
using BLL.DTO;
using BLL.DTO.UpdateTO;
using DAL.Models;
using DAL.Repositories.AppUserRepository;
using BLL.CustomResult;
using AutoMapper;

namespace BLL.Services.AppUserService;

public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IMapper _mapper;

    public AppUserService(IAppUserRepository appUserRepository, IMapper mapper)
    {
        _appUserRepository = appUserRepository;
        _mapper = mapper;
    }
    
    public async Task<IApiResult> CreateAppUserAsync(SignUpModel model)
    {
        AppUser user = _mapper.Map<AppUser>(model);
        HttpStatusCode httpStatusCode = HttpStatusCode.Created;
        string message = "Success";

        try
        {
            user = await _appUserRepository.CreateAppUserAsync(user, model.Password);
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
            user = null;
        }

        return new OperationResult<AppUser>(message, httpStatusCode, user);
    }

    public async Task<IApiResult> DeleteAppUserAsync(string email)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try
        {
            AppUser user = await _appUserRepository.GetAppUserAsync(email);
            await _appUserRepository.DeleteAppUserAsync(user);
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<Object>(message, httpStatusCode);
    }

    public async Task<IApiResult> GetAllAppUserAsync(int page = 1, int count = 10)
    {
        List<AppUser> users = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try
        {
            users = (await _appUserRepository.GetAllAppUserAsync())
                    .Skip((page - 1) * count).Take(count).ToList();
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<List<AppUser>>(message, httpStatusCode, users);
    }

    public async Task<IApiResult> GetAppUserAsync(string email)
    {
        AppUser user = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try
        {
            user = await _appUserRepository.GetAppUserAsync(email);
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<AppUser>(message, httpStatusCode, user);

    }

    public async Task<IApiResult> UpdateAppUserAsync(UpdateModel model)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try
        {
            AppUser user = await _appUserRepository.AuthAppUserAsync(model.AuthData.Email, model.AuthData.Password);

            if(model is AppUserUpdateModel)
            {
                AppUserUpdateModel updModel = (AppUserUpdateModel)model;

                //if(Model.Email == null)
                    //another logic
                if(updModel.PhotoSrc == null)
                    user.PhotoSrc = updModel.PhotoSrc;

                await _appUserRepository.UpdateAppUserAsync(user);
            } else if (model is PasswordUpdateModel)
            {
                PasswordUpdateModel updModel = (PasswordUpdateModel)model;

                await _appUserRepository.UpdatePasswordAsync(user, model.AuthData.Password, updModel.NewPassword);
            }
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<Object>(message, httpStatusCode);
    }

    public async Task<IApiResult> AddToRoleAsync(string email, string role)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";
        try
        {
            await _appUserRepository.AddAppRoleAsync(
                await _appUserRepository.GetAppUserAsync(email),
                role
            );
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<Object>(message, httpStatusCode);
    }

    public async Task<IApiResult> RemoveFromRoleAsync(string email, string role)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";
        try
        {
            await _appUserRepository.RemoveAppRoleAsync(
                await _appUserRepository.GetAppUserAsync(email),
                role
            );
        }catch(Exception e)
        {
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new OperationResult<Object>(message, httpStatusCode);
    }
}
