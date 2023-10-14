using System.Collections.Generic;
using Cafe.Application.ApiResult;
using MediatR;

namespace Cafe.Application.UseCases.BillCases.Get;

public class GetAllBillQuery : IRequest<IApiResult>
{
    public int Page { get; set; } = 0;
    public int Count { get; set; } = 0;

    public GetAllBillQuery(int page, int count)
        => (Page, Count) = (page, count);
}
