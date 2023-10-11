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

    public async Task<IApiResult> InsertTransactionAsync(TransactionCreationDTO model, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if(validationResult.IsValid)
        {   
            Transaction transaction = _mapper.Map<TransactionCreationDTO, Transaction>(model);
            List<Ingridient> ingridients = (await _ingridientRepository.GetAllAsync()).Where(ing => transaction.IngridientsId.Contains(ing.Id)).ToList();

            if(ingridients.Count != transaction.IngridientsId.Count) throw new Exception(Messages.NotFound);

            foreach(var a in ingridients)
            {
                int index = transaction.IngridientsId.IndexOf(a.Id);
                a.Supplies += transaction.Count[index];
                await _ingridientRepository.UpdateAsync(a);
            }
            await _transactionRepository.CreateAsync(transaction);

            return new OperationResult<Transaction>(Messages.Created, HttpStatusCode.Created, transaction);
        }else 
        {
            throw new Exception(validationResult.ToString("|"));
        }
    }

    public async Task<IApiResult> GetUserTransactionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        List<Transaction> result = (await _transactionRepository.GetAllAsync()).Where(t => t.UserId == userId).ToList();
        
        return new OperationResult<List<Transaction>>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IApiResult> GetTransactionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Transaction result = await _transactionRepository.GetByIdAsync(id);
        if(result == null) throw new Exception(Messages.NotFound);

        return new OperationResult<Transaction>(Messages.Success, HttpStatusCode.OK, result);
    }

    public async Task<IApiResult> GetAllTransactionsAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        List<Transaction> result = (await _transactionRepository.GetAllAsync()).Skip(page * count).Take(count).ToList();

        return new OperationResult<List<Transaction>>("", HttpStatusCode.OK, result);
    }
}
