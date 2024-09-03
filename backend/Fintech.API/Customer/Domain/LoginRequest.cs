using System.ComponentModel.DataAnnotations;

namespace Fintech.API.Customer.Domain;

public class LoginRequest(string user, string password)
{
    [Required]
    public string User { get; set; } = user ?? throw new ArgumentNullException(nameof(user));

    [Required]
    public string Password { get; set; } = password ?? throw new ArgumentNullException(nameof(password));
}
