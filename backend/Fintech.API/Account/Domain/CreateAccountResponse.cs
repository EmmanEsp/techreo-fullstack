using System.Text.Json.Serialization;

namespace Fintech.API.Account.Domain;
public class CreateAccountResponse
{
    [JsonPropertyName("accountName")]
    public required string AccountNumber { get; set; }

    [JsonPropertyName("clabe")]
    public required string Clabe { get; set; }

    [JsonPropertyName("balance")]
    public decimal Balance { get; set; }
}
