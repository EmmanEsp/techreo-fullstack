using Fintech.API.Customer.Domain;
using Fintech.API.Customer.Services;

namespace Fintech.API.Customer.UseCases;

class CreateCustomerUseCase : ICreateCustomerUseCase {
    private readonly ICreateCustomerService _service;

    public CreateCustomerUseCase(ICreateCustomerService service)
    {
        _service = service;
    }

    public async Task CreateCustomer(CreateCustomerRequest customer)
    {
        var customerModel = new CustomerModel() {
            Name = customer.Name,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Password = customer.Password
        };
        await _service.CreateCustomer(customerModel);
    }
}
