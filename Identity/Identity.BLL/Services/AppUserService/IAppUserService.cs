using Identity.BLL.DTO;
using Identity.BLL.DTO.UpdateDTO;
using Identity.BLL.CustomResult;

namespace Identity.BLL.Services.AppUserService;

public interface IAppUserService
{
    public Task<IApiResult> CreateAppUserAsync(SignUpModel model);
    public Task<IApiResult> UpdateAppUserAsync(UpdateModel model);
    public Task<IApiResult> DeleteAppUserAsync(string email);
    public Task<IApiResult> GetAppUserAsync(string email);
    public Task<IApiResult> GetAllAppUserAsync(int page = 1, int count = 10);
    public Task<IApiResult> AddToRoleAsync(string email, string role);
    public Task<IApiResult> RemoveFromRoleAsync(string email, string role);
}
