using Fintech.API.Infrastructure;
using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Services;

public class CreateCustomerService : ICreateCustomerService
{
    private readonly MongoDBContext _context;

    public CreateCustomerService(MongoDBContext context)
    {
        _context = context;        
    }

    public async Task CreateCustomer(CustomerModel customer)
    {
        await _context.Customers.InsertOneAsync(customer);
    }
}
