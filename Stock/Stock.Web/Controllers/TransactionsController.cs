using Stock.Application.IServices;
using Stock.Application.DTO.ApiResult;
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
    public async Task<IActionResult> PostAsync([FromBody]TransactionCreationTO model)
    {
        return (await _transactionService.InsertTransactionAsync(model)).Convert();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int page = 0, int count = 10)
    {
        return (await _transactionService.GetAllTransactionsAsync(page, count)).Convert();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return (await _transactionService.GetTransactionAsync(id)).Convert();
    }

    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetByUserAsync(Guid userId)
    {
        return (await _transactionService.GetUserTransactionsAsync(userId)).Convert();
    }
}
