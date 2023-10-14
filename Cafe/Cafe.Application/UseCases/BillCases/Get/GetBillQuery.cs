using System.Collections.Generic;
using Cafe.Application.ApiResult;
using MediatR;

namespace Cafe.Application.UseCases.BillCases.Get;

public class GetBillQuery : IRequest<IApiResult>
{
    public Guid? Id { get; set; }

    public GetBillQuery(Guid id) => Id = id;
}
