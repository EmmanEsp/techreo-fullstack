using System.Text.Json.Serialization;

namespace Fintech.API.Account.Domain;
public class CreateAccountResponse
{
    [JsonPropertyName("accountName")]
    public string AccountNumber { get; set; }

    [JsonPropertyName("clabe")]
    public string Clabe { get; set; }

    [JsonPropertyName("balance")]
    public decimal Balance { get; set; }
}
