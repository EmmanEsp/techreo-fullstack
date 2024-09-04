using Fintech.API.Account.Domain;

namespace Fintech.API.Account.UseCases;

public interface ITransactionUseCase {
    Task<List<TransactionResponse>> GetAllTransactionByCustomerId(Guid customerId);
    Task<UpdatedBalanceResponse> DepositAsync(TransactionRequest transactionRequest);
    Task<UpdatedBalanceResponse> WithdrawAsync(TransactionRequest transactionRequest);
}
