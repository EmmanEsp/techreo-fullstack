using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fintech.API.Account.Domain;

public class TransactionModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("customerId")]
    [BsonRepresentation(BsonType.String)]
    public Guid CustomerId { get; set; }

    [BsonElement("accountId")]
    [BsonRepresentation(BsonType.String)]
    public Guid AccountId { get; set; }

    [BsonElement("type")]
    public required string Type { get; set; }

    private decimal amount;

    [BsonElement("amount")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Amount
    {
        get => amount;
        set
        {
            if (decimal.Round(value, 2) != value)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Amount cannot have more than two decimal places.");
            }
            amount = value;
        }
    }

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; }
}
