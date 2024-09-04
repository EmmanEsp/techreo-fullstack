using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Services;

public interface ITransactionService {
    Task CreateTransactionAsync(TransactionModel transactionModel);
    Task<AccountModel> GetAccountByCustomerIdAsync(Guid customerId);
    Task UpdateAccountBalanceAsync(AccountModel account);
    Task<List<TransactionModel>> GetAllTransactionByCustomerId(Guid customerId);
}
