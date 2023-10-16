using MediatR;
using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;

namespace Cafe.Application.UseCases.BillCases.Create;

public class CreateBillCommand : IRequest<IOperationResult>
{
    public List<Pair<Guid, double>> Dishes { get; set; }
    public double Sale { get; set; } = 0;
}
