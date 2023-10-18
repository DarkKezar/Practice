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
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken token = default)
    {
        var result = await _mediator.Send(new GetEmployeeQuery(id), token);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int page = 1, int count = 10, CancellationToken token = default)
    {
        var result = await _mediator.Send(new GetAllEmployeeQuery(page, count), token);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]CreateEmployeeCommand command, CancellationToken token = default)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }

    [HttpPatch]
    public async Task<IActionResult> CreateAsync([FromBody]UpdateEmployeeCommand command, CancellationToken token = default)
    {
        var result = await _mediator.Send(command, token);

        return result.Convert();
    }
}
