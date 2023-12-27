using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.EmployeeCases.Create;
using Cafe.Application.UseCases.EmployeeCases.Get;
using Cafe.Application.UseCases.EmployeeCases.Update;
using Cafe.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Cafe.Application.OperationResult;
using Cafe.Web.Extenssions;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public EmployeeController(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    } 

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEmployeeAsync(CancellationToken token, Guid id)
    {
        var cacheString = $"employee/{id}";
        var result = await this.GetCachedAsync<OperationResult<GetEmployeeQueryResponse>>(
                                cacheString, _mediator, new GetEmployeeQuery(id), _cache, token);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync(CancellationToken token, [FromQuery]int page = 1, [FromQuery]int count = 10)
    {
        var cacheString = $"employee/{page}:{count}";
        var result = await this.GetCachedAsync<OperationResult<IList<GetEmployeeQueryResponse>>>(
                                cacheString, _mediator, new GetAllEmployeeQuery(page, count), _cache, token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync(CancellationToken token, [FromBody]CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }

    [HttpPatch]
    public async Task<IActionResult> CreateAsync(CancellationToken token, [FromBody]UpdateEmployeeCommand command)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }
}
