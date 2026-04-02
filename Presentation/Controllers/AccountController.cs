using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Accounts.Operations;
using Lab5.Presentation.Http.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Presentation.Http.Controllers;

[ApiController]
[Route("/api/account")]
public sealed class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create")]
    public IActionResult CreateAccount([FromBody] CreateAccountRequest request)
    {
        CreateAccount.Response response = _accountService.CreateAccount(
            new CreateAccount.Request(request.SessionId, request.Pin));

        return response switch
        {
            CreateAccount.Response.Success success => Ok(success.Account),
            CreateAccount.Response.Failure failure => Unauthorized(new { failure.Message }),
            _ => BadRequest(),
        };
    }

    [HttpGet("balance")]
    public IActionResult GetBalance([FromQuery] Guid sessionId)
    {
        try
        {
            GetBalance.Response response = _accountService.GetBalance(
                new GetBalance.Request(sessionId));

            return Ok(response.Balance);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new { ex.Message });
        }
    }

    [HttpPost("deposit")]
    public IActionResult Deposit([FromBody] AmountSessionRequest request)
    {
        try
        {
            Deposit.Response response = _accountService.Deposit(
                new Deposit.Request(request.SessionId, request.Amount));

            return Ok(response.Account);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] AmountSessionRequest request)
    {
        Withdraw.Response response = _accountService.Withdraw(
            new Withdraw.Request(request.SessionId, request.Amount));

        return response switch
        {
            Withdraw.Response.Success success => Ok(success.Account),
            Withdraw.Response.Failure failure => BadRequest(new { failure.Message }),
            _ => BadRequest(),
        };
    }

    [HttpGet("history")]
    public IActionResult GetHistory([FromQuery] Guid sessionId)
    {
        try
        {
            GetHistory.Response response = _accountService.GetHistory(
                new GetHistory.Request(sessionId));

            return Ok(response.Operations);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new { ex.Message });
        }
    }
}
