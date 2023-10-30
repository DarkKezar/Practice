using System.Net;
using Identity.DAL.Models;
using Identity.DAL.Repositories.AppRoleRepository;
using Identity.BLL.OperationResult;
using Identity.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace Identity.BLL.Services.AppRoleService;

public class AppRoleService : IAppRoleService
{
    private readonly IAppRoleRepository _appRoleRepository;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public AppRoleService(IAppRoleRepository appRoleRepository, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _appRoleRepository = appRoleRepository;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IOperationResult> CreateRoleAsync(string name, CancellationToken cancellationToken = default)
    {
        var role = new AppRole(name);
        await _roleManager.CreateAsync(role);
        var result = _mapper.Map<GetAppRoleDTO>(role);

        return new OperationResult<GetAppRoleDTO>(Messages.Created, HttpStatusCode.Created, result);
    }

    public async Task<IOperationResult> DeleteRoleAsync(string name, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByNameAsync(name);
        if(role == null)
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        } 
        await _roleManager.DeleteAsync(role);

        return new OperationResult<Object>(Messages.Success, HttpStatusCode.OK);
    }

    public async Task<IOperationResult> GetRoleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if(role == null)
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        } 
        var result = _mapper.Map<GetAppRoleDTO>(role);
        
        return new OperationResult<GetAppRoleDTO>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IOperationResult> GetRoleAsync(string name, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByNameAsync(name);
        if(role == null)
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        } 
        var result = _mapper.Map<GetAppRoleDTO>(role);
        
        return new OperationResult<GetAppRoleDTO>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IOperationResult> GetRolesAsync(int page = 1, int count = 10, CancellationToken cancellationToken = default)
    {
        var roles = await _appRoleRepository.GetAllAppRoleAsync(page, count, cancellationToken);
        var result = _mapper.Map<IList<GetAppRoleDTO>>(roles);

        return new OperationResult<IList<GetAppRoleDTO>>(Messages.Success, HttpStatusCode.OK, result);
    }
}
