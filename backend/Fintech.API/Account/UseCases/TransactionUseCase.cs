using Fintech.API.Account.Domain;
using Fintech.API.Account.Services;

namespace Fintech.API.Account.UseCases;

public class TransactionUseCase(ITransactionService transactionService) : ITransactionUseCase
{
    private readonly string DEPOSIT = "Deposit";
    private readonly string WITHDRAW = "Withdraw";
    private readonly ITransactionService _transactionService = transactionService;

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
        if(type == WITHDRAW) {
            if (transactionRequest.Amount > account.Balance) {
                throw new ArgumentException("La cantidad a retirar es mayor al balance de la cuenta.");
            }
        }
        var date = DateTime.Now;
        account.UpdatedAt = date;

        var transactionModel = new TransactionModel() {
            CustomerId = transactionRequest.CustomerId,
            AccountId = account.Id,
            Type = type,
            Amount = transactionRequest.Amount,
            CreatedAt = date
        };
        await _transactionService.CreateTransactionAsync(transactionModel);
        return account;
    }
    
    public async Task<TransactionResponse> DepositAsync(TransactionRequest transactionRequest)
    {
        var account = await CreateTransaction(transactionRequest, DEPOSIT);
        account.Balance += transactionRequest.Amount;
        await _transactionService.UpdateAccountBalanceAsync(account);
        return new TransactionResponse() { 
            Type = DEPOSIT, 
            Amount = transactionRequest.Amount, 
            CreatedAt = account.UpdatedAt
        };
    }

    public async Task<TransactionResponse> WithdrawAsync(TransactionRequest transactionRequest)
    {
        var account = await CreateTransaction(transactionRequest, WITHDRAW);
        account.Balance -= transactionRequest.Amount;
        await _transactionService.UpdateAccountBalanceAsync(account);
        return new TransactionResponse() { 
            Type = WITHDRAW,
            Amount = transactionRequest.Amount,
            CreatedAt = account.UpdatedAt
        };
    }
}
