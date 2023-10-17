using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cafe.Application.UseCases.EmployeeCases.Create;
using Cafe.Application.UseCases.EmployeeCases.Get;
using Cafe.Application.UseCases.EmployeeCases.Update;
using Cafe.Domain.Entities;

namespace Cafe.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(CancellationToken token, Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeQuery(id), token);

        return result.Convert();
    }

    [HttpGet]
    [Route("{page}")]
    public async Task<IActionResult> GetAllAsync(CancellationToken token, int page = 1, [FromBody] int count = 10)
    {
        var result = await _mediator.Send(new GetAllEmployeeQuery(page, count), token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CancellationToken token, [FromBody]CreateEmployeeCommand command)
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
