using Cafe.Application.OperationResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Cafe.Web.Extenssions;

public static class ControllerExtensions
{
    public async static Task<IOperationResult> GetCachedAsync<T>(this   ControllerBase controller, 
                                                                        string cacheString,
                                                                        IMediator mediator, 
                                                                        IRequest<IOperationResult> mediatorRequest,
                                                                        IDistributedCache cache,
                                                                        CancellationToken token, 
                                                                        int time = 1) where T : IOperationResult
    {
        var cacheResult = await cache.GetStringAsync(cacheString, token);
        IOperationResult result;
        if(cacheResult == null)
        {
            result = await mediator.Send(mediatorRequest, token);
            await cache.SetStringAsync( cacheString, 
                                        JsonConvert.SerializeObject(result, Formatting.Indented ), 
                                        new DistributedCacheEntryOptions 
                                        { 
                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(time) 
                                        }, 
                                        token);
        }
        else
        {
            result = JsonConvert.DeserializeObject<T>(cacheResult);
        }

        return result;
    }
}
