using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fintech.API.Customer.Domain;

public class CustomerModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("lastName")]
    public string LastName { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }
}
