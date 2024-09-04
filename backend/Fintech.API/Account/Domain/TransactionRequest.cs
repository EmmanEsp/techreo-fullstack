using System.ComponentModel.DataAnnotations;

namespace Fintech.API.Account.Domain;

public class TransactionRequest
{
    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public decimal Amount { get; set; }
}
