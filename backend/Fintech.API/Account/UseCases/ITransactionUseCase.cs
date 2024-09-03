using Fintech.API.Account.Domain;

namespace Fintech.API.Account.UseCases;

public interface ITransactionUseCase {
    Task<decimal> DepositAsync(TransactionRequest transactionRequest);
    Task<decimal> WithdrawAsync(TransactionRequest transactionRequest);
}
