using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.BillCases.Create;
using Cafe.Application.UseCases.BillCases.Get;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BillController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken token = default)
    {
        var result = await _mediator.Send(new GetBillQuery(id), token);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int page = 1, int count = 10, CancellationToken token = default)
    {
        var result = await _mediator.Send(new GetAllBillQuery(page, count), token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]CreateBillCommand command, CancellationToken token = default)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }
}
