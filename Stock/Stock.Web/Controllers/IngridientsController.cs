using Stock.Application.IServices;
using Stock.Application.DTO.OperationResult;
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
    [Route("{page}")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int page = 0, [FromBody] int count = 10)
    {
        var result = await _ingridientService.GetAllIngridientAsync(page, count, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken, Guid id)
    {
        var result = await _ingridientService.GetIngridientAsync(id, cancellationToken);

        return result.Convert();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken, [FromBody] IngridientCreationDTO model)
    {
        var result = await _ingridientService.CreateIngridientAsync(model, cancellationToken);
        
        return result.Convert();
    }
}
