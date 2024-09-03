using Fintech.API.Account.Domain;

namespace Fintech.API.Account.UseCases;

public interface ITransactionUseCase {
    Task<decimal> Deposit(TransactionRequest transactionRequest);
    Task<decimal> Withdraw(TransactionRequest transactionRequest);
}
