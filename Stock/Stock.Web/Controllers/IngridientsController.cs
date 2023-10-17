using Stock.Application.IServices;
using Stock.Application.OperationResult;
using Stock.Application.DTO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Stock.Web.Controllers;

[Controller]
[Route("api/[controller]")]
public class IngridientsController : Controller
{
    private readonly IIngridientService _ingridientService;

    public IngridientsController(IIngridientService ingridientService)
    {
        _ingridientService = ingridientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        var result = await _ingridientService.GetAllIngridientAsync(page, count, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _ingridientService.GetIngridientAsync(id, cancellationToken);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]IngridientCreationDTO model, CancellationToken cancellationToken = default)
    {
        var result = await _ingridientService.CreateIngridientAsync(model, cancellationToken);
        
        return result.Convert();
    }
}
