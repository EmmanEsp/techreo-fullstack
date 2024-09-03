using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fintech.API.Customer.Domain;

public class CustomerModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("lastName")]
    public required string LastName { get; set; }

    [BsonElement("email")]
    public required string Email { get; set; }

    [BsonElement("phone")]
    public required string Phone { get; set; }

    [BsonElement("password")]
    public required string Password { get; set; }
}
