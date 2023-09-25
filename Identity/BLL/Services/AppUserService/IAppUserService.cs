using BLL.DTO;
using BLL.DTO.UpdateTO;
using BLL.CustomResult;

namespace BLL.Services.AppUserService;

public interface IAppUserService
{
    public Task<IApiResult> CreateAppUserAsync(SignUpModel model);
    public Task<IApiResult> UpdateAppUserAsync(UpdateModel model);
    public Task<IApiResult> DeleteAppUserAsync(string email);
    public Task<IApiResult> ReadAppUserAsync(string email);
    public Task<IApiResult> ReadAllAppUserAsync(int page = 1, int count = 10);
    public Task<IApiResult> AddToRoleAsync(string email, string role);
    public Task<IApiResult> RemoveFromRoleAsync(string email, string role);
}
