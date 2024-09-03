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
    public string AccountNumber { get; set; }

    [BsonElement("clabe")]
    public string Clabe { get; set; }

    [BsonElement("balance")]
    [BsonRepresentation(BsonType.Decimal128)]
    [Range(0, 9999999999999999.99)]
    [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Balance can have at most two decimal places.")]
    public decimal Balance { get; set; }
}
