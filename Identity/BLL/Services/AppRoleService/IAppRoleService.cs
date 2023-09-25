using BLL.CustomResult;

namespace BLL.Services.AppRoleService;

public interface IAppRoleService
{
    public Task<IApiResult> CreateRoleAsync(string name);
    public Task<IApiResult> ReadRoleAsync(Guid id);
    public Task<IApiResult> ReadRoleAsync(string name);
    public Task<IApiResult> ReadRolesAsync(int page = 1, int count = 10);
    public Task<IApiResult> DeleteRoleAsync(string name);
}
