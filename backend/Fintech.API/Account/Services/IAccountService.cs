
using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Services;

public interface IAccountService {
    Task CreateAccount(AccountModel account);
    Task<bool> IsAccountNumberUniqueAsync(string accountNumber);
}
