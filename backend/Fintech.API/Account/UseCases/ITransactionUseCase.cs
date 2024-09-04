using Fintech.API.Account.Domain;

namespace Fintech.API.Account.UseCases;

public interface ITransactionUseCase {
    Task<List<TransactionResponse>> GetAllTransactionByCustomerId(Guid customerId);
    Task<TransactionResponse> DepositAsync(TransactionRequest transactionRequest);
    Task<TransactionResponse> WithdrawAsync(TransactionRequest transactionRequest);
}
