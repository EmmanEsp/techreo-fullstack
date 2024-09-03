using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Services;

public interface ICreateCustomerService {
    Task CreateCustomer(CustomerModel customer);
}
