using Stock.Domain.Entities;
using Stock.Application.IServices;
using Stock.Application.Interfaces;
using Stock.Application.DTO;
using Stock.Application.OperationResult;
using Stock.Application.Exceptions;
using Stock.Application.Validators;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FluentValidation;

namespace Stock.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IIngridientRepository _ingridientRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<TransactionCreationDTO> _validator;

    public TransactionService(ITransactionRepository repository, 
                                IIngridientRepository ingridientRepository, 
                                IMapper mapper, 
                                IValidator<TransactionCreationDTO> validator) 
    {
        _transactionRepository = repository;
        _ingridientRepository = ingridientRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IOperationResult> InsertTransactionAsync(TransactionCreationDTO model, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if(!validationResult.IsValid)
        {   
            throw new OperationWebException(validationResult.ToString("|"), (HttpStatusCode)400);
        }

        var transaction = _mapper.Map<TransactionCreationDTO, Transaction>(model);
        var ingridients = _ingridientRepository.GetIQueryable(cancellationToken).Where(ing => transaction.IngridientsId.Contains(ing.Id)).ToList();
        if(ingridients.Count != transaction.IngridientsId.Count)
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        } 

        foreach(var a in ingridients)
        {
            int index = transaction.IngridientsId.IndexOf(a.Id);
            a.Supplies += transaction.Count[index];
            await _ingridientRepository.UpdateAsync(a);
        }
        await _transactionRepository.CreateAsync(transaction, cancellationToken);

        return new OperationResult<Transaction>(Messages.Created, HttpStatusCode.Created, transaction);
    }

    public async Task<IOperationResult> GetUserTransactionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var result = _transactionRepository.GetIQueryable(cancellationToken).Where(t => t.UserId == userId).ToList();
        
        return new OperationResult<IList<Transaction>>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IOperationResult> GetTransactionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _transactionRepository.GetByIdAsync(id, cancellationToken);
        if(result == null) 
        {
            throw new OperationWebException(Messages.NotFound, (HttpStatusCode)404);
        }

        return new OperationResult<Transaction>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IOperationResult> GetAllTransactionsAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        if(page < 0 || count < 0)
        {
            throw new OperationWebException(Messages.BadRequest, (HttpStatusCode)400);
        }
        var result = await _transactionRepository.GetAllAsync(page, count, cancellationToken);

        return new OperationResult<IList<Transaction>>("", HttpStatusCode.OK, result);
    }
}
