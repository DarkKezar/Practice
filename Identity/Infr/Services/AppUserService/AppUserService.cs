using System.Net;
using Core.DTO;
using Core.DTO.UpdateTO;
using Core.Models;
using Core.Repositories.AppUserRepository;
using Infr.CustomResult;

namespace Infr.Services.AppUserService;

public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository appUserRepository;

    public AppUserService(IAppUserRepository appUserRepository){
        this.appUserRepository = appUserRepository;
    }
    
    public async Task<ApiResult> CreateAppUserAsync(SignUpModel model)
    {
        AppUser User = new AppUser(model);
        HttpStatusCode httpStatusCode = HttpStatusCode.Created;
        string message = "Success";

        try{
            await appUserRepository.CreateAppUserAsync(User, model.Password);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
            User = null;
        }

        return new ApiResult(message, httpStatusCode, User);
    }

    public async Task<ApiResult> DeleteAppUserAsync(string email)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try{
            await appUserRepository.DeleteAppUserAsync(
                await appUserRepository.GetAppUserAsync(email)
            );
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode);
    }

    public async Task<ApiResult> ReadAllAppUserAsync(int page = 1, int count = 10)
    {
        List<AppUser> users = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try{
            users = (await appUserRepository.GetAllAppUserAsync())
                    .Skip((page - 1) * count).Take(count).ToList();
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode, users);
    }

    public async Task<ApiResult> ReadAppUserAsync(string email)
    {
        AppUser user = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try{
            user = await appUserRepository.GetAppUserAsync(email);
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode, user);

    }

    public async Task<ApiResult> UpdateAppUserAsync(UpdateModel model)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";

        try{
            AppUser user = await appUserRepository.AuthAppUserAsync(model.AuthData);

            if(model is AppUserUpdateModel){
                AppUserUpdateModel Model = (AppUserUpdateModel)model;

                //if(Model.Email == null)
                    //another logic
                if(Model.PhotoSrc == null)
                    user.PhotoSrc = Model.PhotoSrc;

                await appUserRepository.UpdateAppUserAsync(user);
            } else if (model is PasswordUpdateModel){
                PasswordUpdateModel Model = (PasswordUpdateModel)model;

                await appUserRepository.UpdatePasswordAsync(user, model.AuthData.Password, Model.NewPassword);
            }
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode);
    }

    public async Task<ApiResult> AddToRoleAsync(string email, string role)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";
        try{
            await appUserRepository.AddAppRoleAsync(
                await appUserRepository.GetAppUserAsync(email),
                role
            );
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode);
    }

    public async Task<ApiResult> RemoveFromRoleAsync(string email, string role)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        string message = "Success";
        try{
            await appUserRepository.RemoveAppRoleAsync(
                await appUserRepository.GetAppUserAsync(email),
                role
            );
        }catch(Exception e){
            httpStatusCode = (HttpStatusCode)500;
            message = e.Message;
        }

        return new ApiResult(message, httpStatusCode);
    }
}
