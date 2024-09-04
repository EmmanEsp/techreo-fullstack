using System.Text.Json.Serialization;

public class TransactionResponse
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("amount")]
    public required decimal Amount { get; set; }

    [JsonPropertyName("createdAt")]
    public required DateTime CreatedAt { get; set; }
}
