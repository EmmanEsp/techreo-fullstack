using MongoDB.Driver;
using Microsoft.Extensions.Options;

using Fintech.API.Domain;
using Fintech.API.Customer.Domain;
using Fintech.API.Account.Domain;

namespace Fintech.API.Infrastructure;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }
    public IMongoCollection<CustomerModel> Customers => _database.GetCollection<CustomerModel>("customers");
    public IMongoCollection<AccountModel> Accounts => _database.GetCollection<AccountModel>("accounts");
}
