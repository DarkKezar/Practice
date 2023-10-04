using Stock.Application.IServices;
using Stock.Application.DTO.ApiResult;
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
    public async Task<IActionResult> GetAllAsync(int page = 0, int count = 10)
    {
        return (await _ingridientService.GetAllIngridientAsync(page, count)).Convert();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return (await _ingridientService.GetIngridientAsync(id)).Convert();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]IngridientCreationTO model)
    {
        return (await _ingridientService.CreateIngridientAsync(model)).Convert();
    }
}
