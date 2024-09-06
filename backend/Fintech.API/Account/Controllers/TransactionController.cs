using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Fintech.API.Domain;
using Fintech.API.Account.UseCases;
using Fintech.API.Account.Domain;

namespace Fintech.API.Account.Controllers;

[Authorize]
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
        try 
        {
            var transactions = await _transactionUseCase.GetAllTransactionByCustomerId(customerId);
            var response = SuccessResponse<List<TransactionResponse>>.Success(transactions);
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

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] TransactionRequest transactionRequest)
    {
        try
        {
            var transaction = await _transactionUseCase.DepositAsync(transactionRequest);
            var response = SuccessResponse<TransactionResponse>.Success(transaction);
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

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] TransactionRequest transactionRequest)
    {
        try
        {
            var transaction = await _transactionUseCase.WithdrawAsync(transactionRequest);
            var response = SuccessResponse<TransactionResponse>.Success(transaction);
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
