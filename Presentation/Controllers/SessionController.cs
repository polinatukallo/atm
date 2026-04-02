using Lab5.Application.Contracts.Accounts.Operations;
using Lab5.Application.Contracts.Sessions;
using Lab5.Presentation.Http.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Presentation.Http.Controllers;

[ApiController]
[Route("/api/session")]
public sealed class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("user")]
    public IActionResult CreateUserSession([FromBody] CreateUserSessionRequest request)
    {
        CreateUserSession.Response response = _sessionService.CreateUserSession(
            new CreateUserSession.Request(request.AccountId, request.Pin));

        return response switch
        {
            CreateUserSession.Response.Success success => Ok(success.Session),
            CreateUserSession.Response.Failure failure => Unauthorized(new { failure.Message }),
            _ => BadRequest(),
        };
    }

    [HttpPost("admin")]
    public IActionResult CreateAdminSession([FromBody] CreateAdminSessionRequest request)
    {
        CreateAdminSession.Response response = _sessionService.CreateAdminSession(
            new CreateAdminSession.Request(request.SystemPassword));

        return response switch
        {
            CreateAdminSession.Response.Success success => Ok(success.Session),
            CreateAdminSession.Response.Failure failure => Unauthorized(new { failure.Message }),
            _ => BadRequest(),
        };
    }
}