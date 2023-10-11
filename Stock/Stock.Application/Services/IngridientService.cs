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
using FluentValidation;


namespace Stock.Application.Services;

public class IngridientService : IIngridientService
{
    private readonly IRepository<Ingridient> _ingridientRepository; 
    private readonly IMapper _mapper;
    private readonly IValidator<IngridientCreationDTO> _validator;


    public IngridientService(IRepository<Ingridient> repository, IMapper mapper, IValidator<IngridientCreationDTO> validator)
    {
        _ingridientRepository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IApiResult> CreateIngridientAsync(IngridientCreationDTO model, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if(validationResult.IsValid)
        {
            var ingridient = _mapper.Map<IngridientCreationDTO, Ingridient>(model);
            await _ingridientRepository.CreateAsync(ingridient);

            return new OperationResult<Ingridient>(Messages.Created, HttpStatusCode.Created, ingridient);
        }else 
        {
            throw new Exception(validationResult.ToString("|"));
        }

    }

    public async Task<IApiResult> GetAllIngridientAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        List<Ingridient> result = (await _ingridientRepository.GetAllAsync()).Skip(page * count).Take(count).ToList();

        return new OperationResult<List<Ingridient>>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IApiResult> GetIngridientAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Ingridient result = await _ingridientRepository.GetByIdAsync(id);
        if(result == null) throw new Exception(Messages.NotFound);

        return new OperationResult<Ingridient>(Messages.Success, HttpStatusCode.OK, result);
    }
}
