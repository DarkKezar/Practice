using Stock.Application.IServices;
using Stock.Application.OperationResult;
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
    public async Task<IActionResult> PostAsync([FromBody]TransactionCreationDTO model, CancellationToken cancellationToken = default)
    {
        var result = await _transactionService.InsertTransactionAsync(model, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        var result = await _transactionService.GetAllTransactionsAsync(page, count, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _transactionService.GetTransactionAsync(id, cancellationToken);

        return result.Convert();
    }

    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await _transactionService.GetUserTransactionsAsync(userId, cancellationToken);
        return result.Convert();
    }
}
