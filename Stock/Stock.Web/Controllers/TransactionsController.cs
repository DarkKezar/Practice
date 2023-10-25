using Stock.Application.IServices;
using Stock.Application.DTO.OperationResult;
using Stock.Application.DTO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Stock.Web.Controllers;

[Controller]
[Route("api/[controller]")]
public class TransactionsController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken, [FromBody] TransactionCreationDTO model)
    {
        var result = await _transactionService.InsertTransactionAsync(model, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, [FromQuery]int page = 0, [FromQuery]int count = 10)
    {
        var result = await _transactionService.GetAllTransactionsAsync(page, count, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken, Guid id)
    {
        var result = await _transactionService.GetTransactionAsync(id, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetByUserAsync(CancellationToken cancellationToken, Guid userId)
    {
        var result = await _transactionService.GetUserTransactionsAsync(userId, cancellationToken);

        return result.Convert();
    }
}
