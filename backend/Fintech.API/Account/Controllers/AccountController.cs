using Microsoft.AspNetCore.Mvc;

using Fintech.API.Domain;
using Fintech.API.Account.UseCases;
using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountUseCase _createAccountUseCase;
    
    public AccountController(IAccountUseCase createAccountUseCase)
    {
        _createAccountUseCase = createAccountUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest account)
    {
        var accountResponse = await _createAccountUseCase.CreateAccountAsync(account);
        var response = SuccessResponse<CreateAccountResponse>.Success(accountResponse);
        return Ok(response);
    }
}
