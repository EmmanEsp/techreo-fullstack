using MongoDB.Driver;

using Fintech.API.Infrastructure;
using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Services;

class AccountService : IAccountService
{
    private readonly MongoDBContext _context;

    public AccountService(MongoDBContext context)
    {
        _context = context;        
    }

    public async Task CreateAccount(AccountModel account)
    {
        await _context.Accounts.InsertOneAsync(account);
    }

    public async Task<bool> IsAccountNumberUniqueAsync(string accountNumber)
    {
        var existingAccount = await _context.Accounts.Find(a => a.AccountNumber == accountNumber).FirstOrDefaultAsync();
        return existingAccount == null;
    }

    public async Task<bool> IsClabeUniqueAsync(string clabe)
    {
        var existingClabe = await _context.Accounts.Find(a => a.Clabe == clabe).FirstOrDefaultAsync();
        return existingClabe == null;
    }
}
