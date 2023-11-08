using Stock.Domain.Entities;
using Stock.Application.IServices;
using Stock.Application.Interfaces;
using Stock.Application.DTO;
using Stock.Application.OperationResult;
using Stock.Application.Exceptions;
using AutoMapper;
using System.Net;
using FluentValidation;

namespace Stock.Application.Services;

public class IngridientService : IIngridientService
{
    private readonly IIngridientRepository _ingridientRepository; 
    private readonly IMapper _mapper;
    private readonly IValidator<IngridientCreationDTO> _validator;

    public IngridientService(IIngridientRepository repository, IMapper mapper, IValidator<IngridientCreationDTO> validator)
    {
        _ingridientRepository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IOperationResult> CreateIngridientAsync(IngridientCreationDTO model, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new OperationWebException(validationResult.ToString("|"), (HttpStatusCode)400);   
        }
        var ingridient = _mapper.Map<IngridientCreationDTO, Ingridient>(model);
        await _ingridientRepository.CreateAsync(ingridient, cancellationToken);

        return new OperationResult<Ingridient>(Messages.Created, HttpStatusCode.Created, ingridient);
    }

    public async Task<IOperationResult> GetAllIngridientAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        if(page < 0 || count < 0)
        {
            throw new OperationWebException(Messages.BadRequest, (HttpStatusCode)400);
        }
        var result = await _ingridientRepository.GetAllAsync(page, count, cancellationToken);

        return new OperationResult<IList<Ingridient>>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IOperationResult> GetIngridientAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _ingridientRepository.GetByIdAsync(id, cancellationToken);
        if(result == null) 
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        }

        return new OperationResult<Ingridient>(Messages.Success, HttpStatusCode.OK, result);
    }
}