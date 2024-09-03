using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.UseCases;

public interface ICreateCustomerUseCase {
    Task CreateCustomer(CreateCustomerRequest customer);
}
