using MongoDB.Driver;

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

    public async Task<bool> IsCustomerEmailUniqueAsync(string email)
    {
        var existing = await _context.Customers.Find(a => a.Email == email).FirstOrDefaultAsync();
        return existing == null;
    }

    public async Task<bool> IsCustomerPhoneUniqueAsync(string phone)
    {
        var existing = await _context.Customers.Find(a => a.Phone == phone).FirstOrDefaultAsync();
        return existing == null;
    }

    public async Task<Guid> CreateCustomerAsync(CustomerModel customer)
    {
        await _context.Customers.InsertOneAsync(customer);
        return customer.Id;
    }
}
