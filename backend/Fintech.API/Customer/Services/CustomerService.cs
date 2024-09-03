using Fintech.API.Infrastructure;
using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Services;

public class CustomerService : ICustomerService
{
    private readonly MongoDBContext _context;

    public CustomerService(MongoDBContext context)
    {
        _context = context;        
    }

    public async Task<Guid> CreateCustomerAsync(CustomerModel customer)
    {
        await _context.Customers.InsertOneAsync(customer);
        return customer.Id;
    }
}
