using System.Text.Json.Serialization;

namespace Fintech.API.Account.Domain;
public class UpdatedBalanceResponse
{
    [JsonPropertyName("amount")]
    public required decimal Amount { get; set; }
}
