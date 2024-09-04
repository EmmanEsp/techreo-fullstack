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

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetAllTransaction([FromRoute] Guid customerId)
    {
        var transactions = await _transactionUseCase.GetAllTransactionByCustomerId(customerId);
        var response = Response<List<TransactionResponse>>.Success(transactions);
        return Ok(response);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] TransactionRequest transactionRequest)
    {
        var newBalance = await _transactionUseCase.DepositAsync(transactionRequest);
        var response = Response<UpdatedBalanceResponse>.Success(newBalance);
        return Ok(response);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] TransactionRequest transactionRequest)
    {
        var newBalance = await _transactionUseCase.WithdrawAsync(transactionRequest);
        var response = Response<UpdatedBalanceResponse>.Success(newBalance);
        return Ok(response);
    }
}
