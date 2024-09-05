using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Fintech.API.Domain;
using Fintech.API.Customer.UseCases;
using Fintech.API.Customer.Domain;
using System.Security.Claims;

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
        var response = SuccessResponse<LoginResponse>.Success(loginResponse);
        return Ok(response);
    }

    private string? GetJwtTokenFromHeader()
    {
        var authorizationHeader = Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return null;
        }

        return authorizationHeader.Substring("Bearer ".Length).Trim();
    }

    private Guid? GetCustomerIdFromClaims()
    {
        var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (customerIdClaim == null || !Guid.TryParse(customerIdClaim.Value, out var customerId))
        {
            return null;
        }

        return customerId;
    }
    
    [Authorize]
    [HttpPost("session-data")]
    public async Task<IActionResult> SessionData()
    {
        var token = GetJwtTokenFromHeader();
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Authorization header is missing or invalid.");
        }

        var customerId = GetCustomerIdFromClaims();
        if (customerId == null)
        {
            return Unauthorized("Identification not found in token.");
        }

        var sessionData = await _loginUseCase.GetLoginDataAsync(customerId.Value, token);
        var response = SuccessResponse<LoginResponse>.Success(sessionData);
        return Ok(response);
    }
}
