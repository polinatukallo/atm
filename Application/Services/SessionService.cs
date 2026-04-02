using Lab5.Application.Abstractions.Persistence;
using Lab5.Application.Contracts.Accounts.Operations;
using Lab5.Application.Contracts.Sessions;
using Lab5.Application.Mapping;
using Lab5.Domain.Accounts;
using Lab5.Domain.Sessions;

namespace Lab5.Application.Services;

internal sealed class SessionService : ISessionService
{
    private readonly IPersistenceContext _context;
    private readonly string _systemPassword;

    public SessionService(IPersistenceContext context, string systemPassword)
    {
        _context = context;
        _systemPassword = systemPassword;
    }

    public CreateUserSession.Response CreateUserSession(CreateUserSession.Request request)
    {
        var accountId = new AccountId(request.AccountId);

        Account? account = _context.Accounts.GetById(accountId);

        if (account is null)
            return new CreateUserSession.Response.Failure("Account not found");

        if (account.PinCode.Value != request.Pin)
            return new CreateUserSession.Response.Failure("Invalid PIN");

        var session = new Session(SessionId.New(), SessionType.User, account.Id);
        session = _context.Sessions.Add(session);

        return new CreateUserSession.Response.Success(session.MapToDto());
    }

    public CreateAdminSession.Response CreateAdminSession(CreateAdminSession.Request request)
    {
        if (request.SystemPassword != _systemPassword)
            return new CreateAdminSession.Response.Failure("Invalid system password");

        var session = new Session(SessionId.New(), SessionType.Admin, null);
        session = _context.Sessions.Add(session);

        return new CreateAdminSession.Response.Success(session.MapToDto());
    }
}