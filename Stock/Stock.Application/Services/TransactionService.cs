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
    private readonly IRepository<Transaction> _transactionRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<TransactionCreationDTO> _validator;



    public TransactionService(IRepository<Transaction> repository, IMapper mapper, IValidator<TransactionCreationDTO> validator) 
    {
        _transactionRepository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IApiResult> InsertTransactionAsync(TransactionCreationDTO model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IApiResult> GetUserTransactionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return new OperationResult<List<Transaction>>("", HttpStatusCode.OK,(await _transactionRepository.GetAllAsync()).Where(t => t.UserId == userId).ToList());
    }

    public async Task<IApiResult> GetTransactionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return new OperationResult<Transaction>("", HttpStatusCode.OK, await _transactionRepository.GetByIdAsync(id));
    }

    public async Task<IApiResult> GetAllTransactionsAsync(int page = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        return new OperationResult<List<Transaction>>("", HttpStatusCode.OK, (await _transactionRepository.GetAllAsync()).Skip(page * count).Take(count).ToList());
    }
}
