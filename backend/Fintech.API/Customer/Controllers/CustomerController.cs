using Microsoft.AspNetCore.Mvc;

using Fintech.API.Domain;
using Fintech.API.Customer.UseCases;
using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Controllers;

[ApiController]
[Route("api/v1/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerUseCase _createCustomerUseCase;

    public CustomerController(ICustomerUseCase createCustomerUseCase)
    {
        _createCustomerUseCase = createCustomerUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest customer)
    {
        try {
            var customerResponse = await _createCustomerUseCase.CreateCustomerAsync(customer);
            var response = SuccessResponse<CreateCustomerResponse>.Success(customerResponse);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(BadResponse.Fail(ex.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, BadResponse.Error("Error en el servidor al procesar la solicitud, intentelo mas tarde."));
        }
    }
}
