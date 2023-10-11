using System.Net;
using Identity.BLL.DTO;
using Identity.BLL.DTO.UpdateDTO;
using Identity.DAL.Models;
using Identity.DAL.Repositories.AppUserRepository;
using Identity.BLL.CustomResult;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Identity.BLL.Services.AppUserService;

public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public AppUserService(IAppUserRepository appUserRepository, IMapper mapper, UserManager<AppUser> userManager)
    {
        _appUserRepository = appUserRepository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<IApiResult> CreateAppUserAsync(SignUpModel model)
    {
        AppUser user = _mapper.Map<AppUser>(model);
        var result = await  _userManager.CreateAsync(user, model.Password);

        return new OperationResult<AppUser>(Messages.Created, HttpStatusCode.Created, user);
    }

    public async Task<IApiResult> DeleteAppUserAsync(string email)
    {
        AppUser user = await _appUserRepository.GetAppUserAsync(email);
        if(user == null) throw new Exception(Messages.NoSuchUser);
        user.IsDeleted = true;
        await _userManager.UpdateAsync(user);  


        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IApiResult> GetAllAppUserAsync(int page = 1, int count = 10)
    {
        List<AppUser> users = (await _appUserRepository.GetAllAppUserAsync())
                                    .Skip((page - 1) * count).Take(count).ToList();

        return new OperationResult<List<AppUser>>(Messages.Success, HttpStatusCode.OK, users);
    }

    public async Task<IApiResult> GetAppUserAsync(string email)
    {
        AppUser user = await _appUserRepository.GetAppUserAsync(email);
        if(user == null) throw new Exception(Messages.NoSuchUser);
        return new OperationResult<AppUser>(Messages.Success, HttpStatusCode.OK, user);
    }

    public async Task<IApiResult> UpdateAppUserAsync(UpdateModel model)
    {
        AppUser user = await _appUserRepository.GetAppUserAsync(model.AuthData.Email);
        if(user != null)
        {
            if((await _userManager.CheckPasswordAsync(user, model.AuthData.Password)) == true)
            {
                if(model is AppUserUpdateModel)
                {
                    AppUserUpdateModel updModel = (AppUserUpdateModel)model;

                    //if(Model.Email != null)
                        //another logic
                    if(updModel.PhotoSrc != null)
                        user.PhotoSrc = updModel.PhotoSrc;

                    await _userManager.UpdateAsync(user);
                } else if (model is PasswordUpdateModel)
                {
                    PasswordUpdateModel updModel = (PasswordUpdateModel)model;

                    await _userManager.ResetPasswordAsync(user, model.AuthData.Password, updModel.NewPassword);
                }
            } else 
            {
                throw new Exception(Messages.InvalidPassword);
            }
        } else 
        {
            throw new Exception(Messages.InvalidEmail);
        }
       

        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IApiResult> AddToRoleAsync(string email, string role)
    {
        AppUser user = await _appUserRepository.GetAppUserAsync(email);
        if(user == null) throw new Exception(Messages.NoSuchUser);
        await _userManager.AddToRoleAsync(user, role);

        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IApiResult> RemoveFromRoleAsync(string email, string role)
    {        
        AppUser user = await _appUserRepository.GetAppUserAsync(email);
        if(user == null) throw new Exception(Messages.NoSuchUser);
        await _userManager.RemoveFromRoleAsync(user, role);
        
        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }
}
