using System.ComponentModel.DataAnnotations;

namespace Fintech.API.Account.Domain;

public class TransactionRequest
{
    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero and have up to two decimal places.")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount can have at most two decimal places.")]
    public decimal Amount { get; set; }
}
