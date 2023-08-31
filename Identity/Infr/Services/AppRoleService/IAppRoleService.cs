using Infr.CustomResult;

namespace Infr.Services.AppRoleService;

public interface IAppRoleService
{
    public Task<ApiResult> CreateRoleAsync(string Name);
    public Task<ApiResult> ReadRoleAsync(Guid Id);
    public Task<ApiResult> ReadRoleAsync(string Name);
    public Task<ApiResult> ReadRolesAsync(int page = 1, int count = 10);
    public Task<ApiResult> DeleteRoleAsync(string Name);
}
