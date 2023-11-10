using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.DishCases.Create;
using Cafe.Application.UseCases.DishCases.Delete;
using Cafe.Application.UseCases.DishCases.Get;
using Cafe.Application.UseCases.DishCases.Update;
using Microsoft.Extensions.Caching.Distributed;
using Cafe.Application.OperationResult;
using Cafe.Web.Extenssions;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    
    public DishController(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDishAsync(CancellationToken token, Guid id)
    {
        var cacheString = $"dish/{id}";
        var result = await this.GetCachedAsync<OperationResult<GetDishQueryResponse>>(
                                cacheString, _mediator, new GetDishQuery(id), _cache, token);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDishesAsync(CancellationToken token, [FromQuery]int page = 1, [FromQuery]int count = 10)
    {
        var cacheString = $"dish/{page}:{count}";
        var result = await this.GetCachedAsync<OperationResult<IList<GetDishQueryResponse>>>(
                                cacheString, _mediator, new GetAllDishQuery(page, count), _cache, token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDishAsync(CancellationToken token, [FromBody] CreateDishCommand command)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> CreateAsync(CancellationToken token, Guid id)
    {
        var result = await _mediator.Send(new DeleteDishCommand(id), token);

        return result.Convert();
    }

    [HttpPatch]
    public async Task<IActionResult> CreateAsync(CancellationToken token, [FromBody] UpdateDishCommand command)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }
}
