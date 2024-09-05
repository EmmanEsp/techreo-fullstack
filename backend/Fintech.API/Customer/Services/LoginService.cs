using MongoDB.Driver;

using Fintech.API.Customer.Domain;
using Fintech.API.Infrastructure;
using Fintech.API.Account.Domain;

namespace Fintech.API.Customer.Services;

public class LoginService : ILoginService
{
    private readonly MongoDBContext _context;
    public LoginService(MongoDBContext context)
    {
        _context = context;        
    }

    public async Task<AccountModel> GetAccountByCustomerIdAsync(Guid customerId)
    {
        var account = await _context.Accounts.Find(a => a.CustomerId == customerId).FirstOrDefaultAsync();
        return account;
    }

    public async Task<CustomerModel> IsPhoneInUseAsync(string phone)
    {
        var customer = await _context.Customers.Find(a => a.Phone == phone).FirstOrDefaultAsync();
        return customer;
    }

    public async Task<CustomerModel> IsEmailInUseAsync(string email)
    {
        var customer = await _context.Customers.Find(a => a.Email == email).FirstOrDefaultAsync();
        return customer;
    }

    public async Task<CustomerModel> GetCustomerById(Guid customerId)
    {
        return await _context.Customers.Find(a => a.Id == customerId).FirstOrDefaultAsync();
    }
}
