using Stock.Application.IServices;
using Stock.Application.OperationResult;
using Stock.Application.DTO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Transactions;

namespace Stock.Web.Controllers;

[Controller]
[Route("api/[controller]")]
public class TransactionsController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly IDistributedCache _cache;

    public TransactionsController(ITransactionService transactionService, IDistributedCache cache)
    {
        _transactionService = transactionService;
        _cache = cache;
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
        var cacheString = $"transactions/{id}";
        var cacheResult = await _cache.GetStringAsync(cacheString, cancellationToken);
        IOperationResult result;
        if(cacheResult == null)
        {
            result = await _transactionService.GetTransactionAsync(id, cancellationToken);
            await _cache.SetStringAsync( cacheString, 
                                        JsonConvert.SerializeObject(result, Formatting.Indented), 
                                        new DistributedCacheEntryOptions 
                                        { 
                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) 
                                        }, 
                                        cancellationToken);
        }
        else
        {
            result = JsonConvert.DeserializeObject<OperationResult<Transaction>>(cacheResult);
        }

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
