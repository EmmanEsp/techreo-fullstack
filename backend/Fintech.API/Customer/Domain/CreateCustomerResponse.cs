using System.Text.Json.Serialization;

namespace Fintech.API.Customer.Domain
{
    public class CreateCustomerResponse
    {
        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }
    }
}
