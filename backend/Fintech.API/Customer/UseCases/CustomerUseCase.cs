using Fintech.API.Customer.Domain;
using Fintech.API.Customer.Services;

namespace Fintech.API.Customer.UseCases;

class CustomerUseCase : ICustomerUseCase {
    private readonly ICustomerService _service;

    public CustomerUseCase(ICustomerService service)
    { 
        _service = service;
    }

    public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest customer)
    {
        var customerModel = new CustomerModel() {
            Name = customer.Name,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Password = customer.Password
        };
        var customerId = await _service.CreateCustomerAsync(customerModel);
        return new CreateCustomerResponse() { CustomerId = customerId};
    }
}
