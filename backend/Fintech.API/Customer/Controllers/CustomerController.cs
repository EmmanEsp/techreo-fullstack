using Microsoft.AspNetCore.Mvc;

using Fintech.API.Customer.UseCases;
using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICreateCustomerUseCase _createCustomerUseCase;

    public CustomerController(ICreateCustomerUseCase createCustomerUseCase)
    {
        _createCustomerUseCase = createCustomerUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest customer)
    {
        await _createCustomerUseCase.CreateCustomer(customer);
        return Ok();
    }
}
