using Fintech.API.Account.Domain;
using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Services;

public interface ILoginService {
    Task<CustomerModel> IsPhoneInUseAsync(string phone);
    Task<CustomerModel> IsEmailInUseAsync(string email);
    Task<AccountModel> GetAccountByCustomerIdAsync(Guid customerId);
}
