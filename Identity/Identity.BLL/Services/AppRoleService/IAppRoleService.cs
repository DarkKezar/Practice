using Identity.BLL.CustomResult;

namespace Identity.BLL.Services.AppRoleService;

public interface IAppRoleService
{
    public Task<IApiResult> CreateRoleAsync(string name);
    public Task<IApiResult> GetRoleAsync(Guid id);
    public Task<IApiResult> GetRoleAsync(string name);
    public Task<IApiResult> GetRolesAsync(int page = 1, int count = 10);
    public Task<IApiResult> DeleteRoleAsync(string name);
}
