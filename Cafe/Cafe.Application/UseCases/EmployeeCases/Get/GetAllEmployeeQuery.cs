using System.Collections.Generic;
using Cafe.Application.ApiResult;
using MediatR;

namespace Cafe.Application.UseCases.EmployeeCases.Get;

public class GetAllEmployeeQuery : IRequest<IApiResult>
{
    public int Page { get; set; } = 0;
    public int Count { get; set; } = 0;

    public GetAllEmployeeQuery(int page, int count)
        => (Page, Count) = (page, count);
}
