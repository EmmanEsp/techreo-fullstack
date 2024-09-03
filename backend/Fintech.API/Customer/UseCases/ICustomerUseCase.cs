using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.UseCases;

public interface ICustomerUseCase {
    Task<CreateCustomerResponse> CreateCustomer(CreateCustomerRequest customer);
}
