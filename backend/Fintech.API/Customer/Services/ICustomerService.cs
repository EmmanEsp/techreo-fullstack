using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Services;

public interface ICustomerService {
    Task<Guid> CreateCustomerAsync(CustomerModel customer);
}
