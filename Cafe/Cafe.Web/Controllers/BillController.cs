using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.BillCases.Create;
using Cafe.Application.UseCases.BillCases.Get;
using Cafe.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Cafe.Web.Hubs;
using Cafe.Application.OperationResult;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Cafe.Web.Extenssions;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<BillHub> _hubContext;
    private readonly IDistributedCache _cache;
    
    public BillController(IMediator mediator, IHubContext<BillHub> hubContext, IDistributedCache cache) 
    {
        _mediator = mediator;
        _hubContext = hubContext;
        _cache = cache;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetBillAsync(CancellationToken token, Guid id)
    {
        var cacheString = $"bills/{id}";
        var result = await this.GetCachedAsync<OperationResult<GetBillQueryResponse>>(
                                cacheString, _mediator, new GetBillQuery(id), _cache, token);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBillsAsync(CancellationToken token,  [FromQuery] int page = 1, [FromQuery] int count = 10)
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
