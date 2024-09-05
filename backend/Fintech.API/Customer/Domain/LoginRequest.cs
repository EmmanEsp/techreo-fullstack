using System.ComponentModel.DataAnnotations;

namespace Fintech.API.Customer.Domain;

public class LoginRequest(string user, string password)
{
    [Required(ErrorMessage = "El usuario es requerido.")]
    public string User { get; set; } = user;

    [Required(ErrorMessage = "La contraseña es requerida.")]
    public string Password { get; set; } = password;
}
