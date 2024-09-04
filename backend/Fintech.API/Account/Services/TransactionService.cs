using MongoDB.Driver;

using Fintech.API.Infrastructure;
using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Services;

public class TransactionService : ITransactionService
{
    private readonly MongoDBContext _context;

    public TransactionService(MongoDBContext context)
    {
        _context = context;        
    }

    public async Task UpdateBalanceAsync(Guid accountId, decimal amount)
    {
        var filter = Builders<AccountModel>.Filter.Eq(a => a.Id, accountId);
        var update = Builders<AccountModel>.Update.Set(a => a.Balance, amount);
        await _context.Accounts.UpdateOneAsync(filter, update);
    }

    public async Task<AccountModel> GetAccountByCustomerIdAsync(Guid customerId)
    {
        var account = await _context.Accounts.Find(a => a.CustomerId == customerId).FirstOrDefaultAsync();
        return account;
    }
    
    public async Task CreateTransactionAsync(TransactionModel transactionModel)
    {
        await _context.Transactions.InsertOneAsync(transactionModel);
    }
}
