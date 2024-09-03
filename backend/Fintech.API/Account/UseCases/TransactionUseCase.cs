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
    
    public async Task<decimal> Deposit(TransactionRequest transactionRequest)
    {
        var account = await CreateTransaction(transactionRequest, "Deposit");
        var newBalance = account.Balance + transactionRequest.Amount;
        await _transactionService.UpdateBalanceAsync(account.Id, newBalance);
        return newBalance;
    }

    public async Task<decimal> Withdraw(TransactionRequest transactionRequest)
    {
        var account = await CreateTransaction(transactionRequest, "Withdraw");
        var newBalance = account.Balance - transactionRequest.Amount;
        await _transactionService.UpdateBalanceAsync(account.CustomerId, newBalance);
        return newBalance;
    }
}
