using System.ComponentModel.DataAnnotations;

namespace Fintech.API.Customer.Domain;

public class CreateCustomerRequest
{
    [Required(ErrorMessage = "El nombre es requerido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre deben tener una longitud minima de 2 caracteres.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Los apellidos son requeridos.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener una longitud minima de 2 caracteres.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "El email es requerido.")]
    [EmailAddress(ErrorMessage = "El email no es valido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El Celular es requerido.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El celular debe ser exactamente 10 digitos.")]
    [Phone(ErrorMessage = "El celular no es un numero valido.")]
    public required string Phone { get; set; }

    [Required(ErrorMessage = "La password es requerida.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "La password debe ser minimo 6 caracteres y maximo 20")]
    public required string Password { get; set; }
}
