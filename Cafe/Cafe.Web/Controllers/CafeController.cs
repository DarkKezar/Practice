using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.BillCases.Get;
using Cafe.Domain.Entities;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CafeController : ControllerBase
{
    private readonly IMediator _mediator;
    public CafeController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get(int page = 1, int count = 10, CancellationToken token = default)
    {
        var result = await _mediator.Send(new GetAllBillQuery(page, count), token);
        return result.Convert();
    }
}
