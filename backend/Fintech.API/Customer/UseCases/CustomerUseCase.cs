using Fintech.API.Domain;
using Fintech.API.Customer.Domain;
using Fintech.API.Customer.Services;

namespace Fintech.API.Customer.UseCases;

class CustomerUseCase : ICustomerUseCase {
    private readonly ICustomerService _service;

    public CustomerUseCase(ICustomerService service)
    { 
        _service = service;
    }

    private string HashPassword(string password) 
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest customer)
    {
        var isCustomerEmailUnique = await _service.IsCustomerEmailUniqueAsync(customer.Email);
        if (!isCustomerEmailUnique) {
            throw new ArgumentException("El Email ya esta en uso.");
        }
        
        var isCustomerPhoneUnique = await _service.IsCustomerPhoneUniqueAsync(customer.Phone);
        if (!isCustomerPhoneUnique) {
            throw new ArgumentException("El celular ya esta en uso.");
        }

        var customerModel = new CustomerModel() {
            Name = customer.Name,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Password = HashPassword(customer.Password)
        };
        var customerId = await _service.CreateCustomerAsync(customerModel);
        return new CreateCustomerResponse() { CustomerId = customerId};
    }
}
