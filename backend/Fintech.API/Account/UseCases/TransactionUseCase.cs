using System;

using Fintech.API.Account.Domain;
using Fintech.API.Account.Services;

namespace Fintech.API.Account.UseCases;

public class TransactionUseCase : ITransactionUseCase
{
    private readonly ITransactionService _transactionService;
    public TransactionUseCase(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    public TransactionResponse MapToTransactionResponse(TransactionModel transaction)
    {
        return new TransactionResponse
        {
            Type = transaction.Type,
            Amount = transaction.Amount,
            CreatedAt = transaction.CreatedAt
        };
    }

    public async Task<List<TransactionResponse>> GetAllTransactionByCustomerId(Guid customerId)
    {
        var transactions = await _transactionService.GetAllTransactionByCustomerId(customerId);
        return transactions.Select(MapToTransactionResponse).ToList();
    }
    
    private async Task<AccountModel> CreateTransaction(TransactionRequest transactionRequest, string type)
    {
        var account = await _transactionService.GetAccountByCustomerIdAsync(transactionRequest.CustomerId);
        var transactionModel = new TransactionModel() {
            CustomerId = transactionRequest.CustomerId,
            AccountId = account.Id,
            Type = type,
            Amount = transactionRequest.Amount
        };
        await _transactionService.CreateTransactionAsync(transactionModel);
        return account;
    }
    
    public async Task<UpdatedBalanceResponse> DepositAsync(TransactionRequest transactionRequest)
    {
        var account = await CreateTransaction(transactionRequest, "Deposit");
        var newBalance = account.Balance + transactionRequest.Amount;
        await _transactionService.UpdateBalanceAsync(account.Id, newBalance);
        return new UpdatedBalanceResponse() { Amount = newBalance };
    }

    public async Task<UpdatedBalanceResponse> WithdrawAsync(TransactionRequest transactionRequest)
    {
        var account = await CreateTransaction(transactionRequest, "Withdraw");
        var newBalance = account.Balance - transactionRequest.Amount;
        await _transactionService.UpdateBalanceAsync(account.Id, newBalance);
        return new UpdatedBalanceResponse() { Amount = newBalance };
    }
}
