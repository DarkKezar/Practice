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


    public IngridientService(IRepository<Ingridient> repository, IMapper mapper)
    {
        _ingridientRepository = repository;
        _mapper = mapper;
    }

    public async Task<IApiResult> CreateIngridientAsync(IngridientCreationDTO model, CancellationToken cancellationToken = default)
    {
        var validationResult = await (new IngridientCreationDTOValidator()).ValidateAsync(model);
        if(validationResult.IsValid)
        {
            var ingridient = _mapper.Map<IngridientCreationDTO, Ingridient>(model);

            return new OperationResult<Ingridient>("", HttpStatusCode.Created, await _ingridientRepository.CreateAsync(ingridient));
        }

        return new OperationResult<List<FluentValidation.Results.ValidationFailure>>("", HttpStatusCode.BadRequest, validationResult.Errors);
    }

    public async Task<IApiResult> GetAllIngridientAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        return new OperationResult<List<Ingridient>>("", HttpStatusCode.OK, (await _ingridientRepository.GetAllAsync()).Skip(page * count).Take(count).ToList());
    }

    public async Task<IApiResult> GetIngridientAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return new OperationResult<Ingridient>("", HttpStatusCode.OK, await _ingridientRepository.GetByIdAsync(id));
    }
}
