using System.ComponentModel.DataAnnotations;

namespace Fintech.API.Customer.Domain;

public class CreateCustomerRequest
{
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; set; }

    [StringLength(100, MinimumLength = 2)]
    public required string LastName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [Phone]
    public required string Phone { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public required string Password { get; set; }
}
