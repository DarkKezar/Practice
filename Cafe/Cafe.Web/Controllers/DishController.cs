using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.DishCases.Create;
using Cafe.Application.UseCases.DishCases.Delete;
using Cafe.Application.UseCases.DishCases.Get;
using Cafe.Application.UseCases.DishCases.Update;
using Cafe.Domain.Entities;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DishController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(CancellationToken token, Guid id)
    {
        var result = await _mediator.Send(new GetDishQuery(id), token);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken token, [FromQuery]int page = 1, [FromQuery]int count = 10)
    {
        var result = await _mediator.Send(new GetAllDishQuery(page, count), token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CancellationToken token, [FromBody] CreateDishCommand command)
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
