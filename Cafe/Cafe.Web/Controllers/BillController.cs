using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.BillCases.Create;
using Cafe.Application.UseCases.BillCases.Get;
using Cafe.Domain.Entities;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BillController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(CancellationToken token, Guid id)
    {
        var result = await _mediator.Send(new GetBillQuery(id), token);

        return result.Convert();
    }

    [HttpGet]
    [Route("{page}")]
    public async Task<IActionResult> GetAllAsync(CancellationToken token, int page = 1, [FromBody] int count = 10)
    {
        var result = await _mediator.Send(new GetAllBillQuery(page, count), token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CancellationToken token, [FromBody] CreateBillCommand command)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }
}
