using MediatR;
using Cafe.Domain.Entities;
using Cafe.Application.ApiResult;

namespace Cafe.Application.UseCases.BillCases.Create;

public class CreateBillCommand : IRequest<IApiResult>
{
    public List<Pair<Guid, double>> Dishes { get; set; }
    public double Sale { get; set; } = 0;

}
