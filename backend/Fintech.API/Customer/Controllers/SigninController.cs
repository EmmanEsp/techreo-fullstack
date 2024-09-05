using Microsoft.AspNetCore.Mvc;

using Fintech.API.Domain;
using Fintech.API.Customer.UseCases;
using Fintech.API.Customer.Domain;

namespace Fintech.API.Customer.Controllers;

[ApiController]
[Route("api/v1/signin")]
public class SigninController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    public SigninController(ILoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        var loginResponse = await _loginUseCase.LoginAsync(login);
        var response = ServiceResponse<LoginResponse>.Success(loginResponse);
        return Ok(response);
    }
}
