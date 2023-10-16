using System.Collections.Generic;
using Cafe.Application.OperationResult;
using MediatR;

namespace Cafe.Application.UseCases.BillCases.Get;

public class GetBillQuery : IRequest<IOperationResult>
{
    public Guid? Id { get; set; }

    public GetBillQuery(Guid id) => Id = id;
}
