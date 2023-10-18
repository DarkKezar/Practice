using Stock.Domain.Entities;
using Stock.Application.DTO;
using Stock.Application.DTO.OperationResult;

namespace Stock.Application.IServices;

public interface IIngridientService
{
    Task<IOperationResult> CreateIngridientAsync(IngridientCreationDTO model, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetAllIngridientAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default);
    Task<IOperationResult> GetIngridientAsync(Guid id, CancellationToken cancellationToken = default);
}