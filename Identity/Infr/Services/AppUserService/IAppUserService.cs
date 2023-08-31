using Core.DTO;
using Core.DTO.UpdateTO;
using Infr.CustomResult;

namespace Infr.Services.AppUserService;

public interface IAppUserService
{
    public Task<ApiResult> CreateAppUserAsync(SignUpModel model);
    public Task<ApiResult> UpdateAppUserAsync(UpdateModel model);
    public Task<ApiResult> DeleteAppUserAsync(string email);
    public Task<ApiResult> ReadAppUserAsync(string email);
    public Task<ApiResult> ReadAllAppUserAsync(int page = 1, int count = 10);
    public Task<ApiResult> AddToRoleAsync(string email, string role);
    public Task<ApiResult> RemoveFromRoleAsync(string email, string role);
}
