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
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken token = default)
    {
        var result = await _mediator.Send(new GetBillQuery(id), token);
        await _hubContext.Clients.All.SendAsync("Bills", "server", "mat vashy");

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int page = 1, int count = 10, CancellationToken token = default)
    {
        var result = await _mediator.Send(new GetAllBillQuery(page, count), token);
        await _hubContext.Clients.All.SendAsync("Bills", "server", "mat vashy");

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]CreateBillCommand command, CancellationToken token = default)
    {
        var result = await _mediator.Send(command, token);
        await _hubContext.Clients.All.SendAsync("Bills", "server", JsonSerializer.Serialize(((OperationResult<Bill>)result).ObjectResult));

        return result.Convert();
    }
}
