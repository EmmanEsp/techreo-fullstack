using System.ComponentModel.DataAnnotations;

namespace Fintech.API.Account.Domain;

public class CreateAccountRequest
{
    [Required]
    public Guid CustomerId { get; set; }
}
