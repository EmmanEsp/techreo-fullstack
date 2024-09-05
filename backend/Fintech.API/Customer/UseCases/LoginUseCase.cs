using BCrypt;

using Fintech.API.Customer.Domain;
using Fintech.API.Customer.Services;

namespace Fintech.API.Customer.UseCases;

public class LoginUseCase(ILoginService loginService) : ILoginUseCase
{
    private readonly ILoginService _loginService = loginService;

    public bool VerifyPassword(string password, string hashedPassword) {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var customer = await _loginService.IsEmailInUseAsync(loginRequest.User);
        customer ??= await _loginService.IsPhoneInUseAsync(loginRequest.User);

        if (customer == null) {
            throw new ArgumentException("Customer not found");
        }

        if (!VerifyPassword(loginRequest.Password, customer.Password)) 
        {
            throw new ArgumentException("Invalid password");
        }
        
        var accountModel = await _loginService.GetAccountByCustomerIdAsync(customer.Id);
        return new LoginResponse() {
            CustomerId = customer.Id,
            Name = customer.Name,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            AccountNumber = accountModel.AccountNumber,
            Balance = accountModel.Balance,
            Clabe = accountModel.Clabe
        };
    }
}
