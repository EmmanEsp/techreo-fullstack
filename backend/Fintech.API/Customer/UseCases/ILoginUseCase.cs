using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.UseCases;

public interface ILoginUseCase {
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
}
