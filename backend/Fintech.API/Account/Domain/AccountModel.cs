using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fintech.API.Account.Domain;

public class AccountModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("customerId")]
    [BsonRepresentation(BsonType.String)]
    public Guid CustomerId { get; set; }

    [BsonElement("accountNumber")]
    public required string AccountNumber { get; set; }

    [BsonElement("clabe")]
    public required string Clabe { get; set; }

    [BsonElement("balance")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Balance { get; set; }
}
