using Fintech.API.Account.Domain;

namespace Fintech.API.Account.UseCases;

public interface IAccountUseCase {
    Task<CreateAccountResponse> CreateAccount(CreateAccountRequest account);
}
