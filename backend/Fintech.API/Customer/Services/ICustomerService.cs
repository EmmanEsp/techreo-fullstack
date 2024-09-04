using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Services;

public interface ICustomerService {
    Task<Guid> CreateCustomerAsync(CustomerModel customer);
    Task<bool> IsCustomerEmailUniqueAsync(string email);
    Task<bool> IsCustomerPhoneUniqueAsync(string phone);
}
