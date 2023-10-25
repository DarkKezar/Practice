using System.Net;
using Identity.BLL.DTO;
using Identity.DAL.Models;
using Identity.DAL.Repositories.AppUserRepository;
using Identity.BLL.OperationResult;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Identity.DLL.Proto;
using static Identity.DLL.Proto.AccountCreation;

namespace Identity.BLL.Services.AppUserService;

public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    //private readonly GrpcChannel _channel;
    private readonly AccountCreationClient _client;

    //public AppUserService(IAppUserRepository appUserRepository, IMapper mapper, UserManager<AppUser> userManager, GrpcChannel channel)
    public AppUserService(IAppUserRepository appUserRepository, IMapper mapper, UserManager<AppUser> userManager, AccountCreationClient client)
    {
        _appUserRepository = appUserRepository;
        _userManager = userManager;
        _mapper = mapper;
        _client = client;
    }
    
    public async Task<IOperationResult> CreateAppUserAsync(SignUpModel model, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<AppUser>(model);
        await  _userManager.CreateAsync(user, model.Password);

        var sendModel = _mapper.Map<AccountRequest>(model);
        sendModel.IdentityIdString = user.Id.ToString();
        //var client = new AccountCreation.AccountCreationClient(_channel);
        var reply = await _client.CreateAccountAsync(sendModel);

        var result = _mapper.Map<GetAppUserDTO>(user);

        return new OperationResult<GetAppUserDTO>(Messages.Created, HttpStatusCode.Created, result);
    }

    public async Task<IOperationResult> DeleteAppUserAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserAsync(email, cancellationToken);
        if(user == null)
        {
            throw new OperationWebException(Messages.NoSuchUser, (HttpStatusCode)404);
        } 
        user.IsDeleted = true;
        await _userManager.UpdateAsync(user);  


        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IOperationResult> GetAllAppUserAsync(int page = 1, int count = 10, CancellationToken cancellationToken = default)
    {
        var users = await _appUserRepository.GetAllAppUserAsync(page, count, cancellationToken);
        var result = _mapper.Map<IList<GetAppUserDTO>>(users);

        return new OperationResult<IList<GetAppUserDTO>>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IOperationResult> GetAppUserAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserAsync(email, cancellationToken);
        if(user == null)
        {
            throw new OperationWebException(Messages.NoSuchUser, (HttpStatusCode)404);
        } 
        var result = _mapper.Map<GetAppUserDTO>(user);

        return new OperationResult<GetAppUserDTO>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IOperationResult> UpdateAppUserAsync(Guid userId, AppUserUpdateModel model, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if(user == null)
        {
            throw new OperationWebException(Messages.NoSuchUser, (HttpStatusCode)404);
        } 
        _mapper.Map(model, user);
        await _userManager.UpdateAsync(user);
        
        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IOperationResult> UpdateAppUserPasswrodAsync(Guid userId, PasswordUpdateModel model, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if(user == null)
        {
            throw new OperationWebException(Messages.NoSuchUser, (HttpStatusCode)404);
        }
        await _userManager.ResetPasswordAsync(user, model.OldPassword, model.NewPassword);

        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IOperationResult> AddToRoleAsync(string email, string role, CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserAsync(email, cancellationToken);
        if(user == null)
        {
            throw new OperationWebException(Messages.NoSuchUser, (HttpStatusCode)404);
        } 
        await _userManager.AddToRoleAsync(user, role);

        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IOperationResult> RemoveFromRoleAsync(string email, string role, CancellationToken cancellationToken = default)
    {        
        var user = await _appUserRepository.GetAppUserAsync(email, cancellationToken);
        if(user == null)
        {
            throw new OperationWebException(Messages.NoSuchUser, (HttpStatusCode)404);
        } 
        await _userManager.RemoveFromRoleAsync(user, role);
        
        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }
}
