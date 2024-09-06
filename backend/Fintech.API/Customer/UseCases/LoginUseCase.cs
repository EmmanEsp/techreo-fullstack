using BCrypt;

using Fintech.API.Customer.Domain;
using Fintech.API.Customer.Services;
using Fintech.API.Services;

namespace Fintech.API.Customer.UseCases;

public class LoginUseCase(ILoginService loginService, IJwtService token) : ILoginUseCase
{
    private readonly ILoginService _loginService = loginService;
    private readonly IJwtService _token = token;

    public bool VerifyPassword(string password, string hashedPassword) {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var customer = await _loginService.IsEmailInUseAsync(loginRequest.User);
        customer ??= await _loginService.IsPhoneInUseAsync(loginRequest.User);

        if (customer == null) {
            throw new ArgumentException("Usuario no encontrado.");
        }

        if (!VerifyPassword(loginRequest.Password, customer.Password)) 
        {
            throw new ArgumentException("La password no coincide.");
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
            Clabe = accountModel.Clabe,
            Token = _token.GenerateJwtToken(customer.Id)
        };
    }

    public async Task<LoginResponse> GetLoginDataAsync(Guid customerId, string token)
    {
        var customer = await _loginService.GetCustomerById(customerId);
        var account = await _loginService.GetAccountByCustomerIdAsync(customer.Id);
        return new LoginResponse() {
            CustomerId = customer.Id,
            Name = customer.Name,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Clabe = account.Clabe,
            Token = token
        };
    }
}
