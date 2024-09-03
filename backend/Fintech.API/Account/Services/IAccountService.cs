
using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Services;

public interface IAccountService {
    Task CreateAccountAsync(AccountModel account);
    Task<bool> IsAccountNumberUniqueAsync(string accountNumber);
}
