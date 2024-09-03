using System.Text.Json.Serialization;

namespace Fintech.API.Customer.Domain;

public class LoginResponse
{
    [JsonPropertyName("customerId")]
    public required Guid CustomerId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("phone")]
    public required string Phone { get; set; }

    [JsonPropertyName("accountNumber")]
    public required string AccountNumber { get; set; }

    [JsonPropertyName("clabe")]
    public required string Clabe { get; set; }

    [JsonPropertyName("balance")]
    public required decimal Balance { get; set; }
}
