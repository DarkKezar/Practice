using Stock.Domain.Entities;
using Stock.Application.DTO;
using Stock.Application.DTO.ApiResult;

namespace Stock.Application.IServices;

public interface IIngridientService
{
    Task<IApiResult> CreateIngridientAsync(IngridientCreationDTO model, CancellationToken cancellationToken = default);
    Task<IApiResult> GetAllIngridientAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default);
    Task<IApiResult> GetIngridientAsync(Guid id, CancellationToken cancellationToken = default);
}
