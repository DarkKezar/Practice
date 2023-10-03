using Stock.Domain.Entities;
using Stock.Application.IServices;
using Stock.Application.Interfaces;
using Stock.Application.DTO;
using Stock.Application.DTO.ApiResult;
using Stock.Application.Validators;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace Stock.Application.Services;

public class IngridientService : IIngridientService
{
    private readonly IRepository<Ingridient> _ingridientRepository; 
    private readonly IMapper _mapper;
    private TimeSpan _timeout { get; }


    public IngridientService(IRepository<Ingridient> repository, IMapper mapper, TimeSpan timeout)
    {
        _ingridientRepository = repository;
        _mapper = mapper;
        _timeout = timeout;
    }

    public async Task<IApiResult> CreateIngridientAsync(IngridientCreationTO model, CancellationToken cancellationToken = default)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource((new CancellationTokenSource()).Token, cancellationToken);
        cts.CancelAfter(_timeout);

        var validationResult = await (new IngridientCTOValidator()).ValidateAsync(model);
        if(validationResult.IsValid)
        {
            var ingridient = _mapper.Map<IngridientCreationTO, Ingridient>(model);

            return new ApiObjectResult<Ingridient>("", HttpStatusCode.Created, await _ingridientRepository.CreateAsync(ingridient));
        }

        return new ApiObjectResult<List<FluentValidation.Results.ValidationFailure>>("", HttpStatusCode.BadRequest, validationResult.Errors);
    }

    public async Task<IApiResult> GetAllIngridientAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource((new CancellationTokenSource()).Token, cancellationToken);
        cts.CancelAfter(_timeout);

        return new ApiObjectResult<List<Ingridient>>("", HttpStatusCode.OK, (await _ingridientRepository.GetAllAsync()).Skip(page * count).Take(count).ToList());
    }

    public async Task<IApiResult> GetIngridientAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource((new CancellationTokenSource()).Token, cancellationToken);
        cts.CancelAfter(_timeout);
        
        return new ApiObjectResult<Ingridient>("", HttpStatusCode.OK, await _ingridientRepository.GetByIdAsync(id));
    }
}
