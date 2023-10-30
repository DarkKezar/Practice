using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.BillCases.Create;
using Cafe.Application.UseCases.BillCases.Get;
using Cafe.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Cafe.Web.Hubs;
using Cafe.Application.OperationResult;
using System.Text.Json;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<BillHub> _hubContext;
    
    public BillController(IMediator mediator, IHubContext<BillHub> hubContext) 
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetBillAsync(CancellationToken token, Guid id)
    {
        var result = await _mediator.Send(new GetBillQuery(id), token);

        return result.Convert();
    }

    [HttpGet]
    [Route("{page}")]
    public async Task<IActionResult> GetAllBillsAsync(CancellationToken token, int page = 1, [FromBody] int count = 10)
    {
        var result = await _mediator.Send(new GetAllBillQuery(page, count), token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBillAsync(CancellationToken token, [FromBody] CreateBillCommand command)
    {
        var result = await _mediator.Send(command, token);
        await _hubContext.Clients.All.SendAsync("Bills", "server", JsonSerializer.Serialize(((OperationResult<Bill>)result).ObjectResult));

        return result.Convert();
    }
}
