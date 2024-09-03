using Microsoft.AspNetCore.Mvc;

using Fintech.API.Domain;
using Fintech.API.Account.UseCases;
using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Controllers;

[ApiController]
[Route("api/v1/transaction")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionUseCase _transactionUseCase;
    
    public TransactionController(ITransactionUseCase transactionUseCase)
    {
        _transactionUseCase = transactionUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Deposit([FromBody] TransactionRequest transactionRequest)
    {
        var newBalance = await _transactionUseCase.DepositAsync(transactionRequest);
        var response = Response<decimal>.Success(newBalance);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Withdraw([FromBody] TransactionRequest transactionRequest)
    {
        var newBalance = await _transactionUseCase.WithdrawAsync(transactionRequest);
        var response = Response<decimal>.Success(newBalance);
        return Ok(response);
    }
}
