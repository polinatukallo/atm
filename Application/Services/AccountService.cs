using Lab5.Application.Abstractions.Persistence;
using Lab5.Application.Abstractions.Persistence.Queries;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Accounts.Models;
using Lab5.Application.Contracts.Accounts.Operations;
using Lab5.Application.Mapping;
using Lab5.Domain.Accounts;
using Lab5.Domain.Operations;
using Lab5.Domain.Sessions;
using Lab5.Domain.ValueObjects;

namespace Lab5.Application.Services;

public sealed class AccountService : IAccountService
{
    private readonly IPersistenceContext _context;

    public AccountService(IPersistenceContext context)
    {
        _context = context;
    }

    public CreateAccount.Response CreateAccount(CreateAccount.Request request)
    {
        Session? session = _context.Sessions.GetSessionId(new SessionId(request.SessionId));

        if (session is null)
            return new CreateAccount.Response.Failure("Session not found");

        if (session.Type != SessionType.Admin)
            return new CreateAccount.Response.Failure("Not authorized to create account");

        var account = new Account(
            AccountId.Default,
            new PinCode(request.Pin),
            new Money(0));

        account = _context.Accounts.Add(account);

        return new CreateAccount.Response.Success(account.MapToDto());
    }

    public GetBalance.Response GetBalance(GetBalance.Request request)
    {
        Account account = GetAccountForSession(request.SessionId);
        return new GetBalance.Response(account.Balance.Value);
    }

    public Deposit.Response Deposit(Deposit.Request request)
    {
        Account account = GetAccountForSession(request.SessionId);

        var amount = new Money(request.Amount);
        account.Deposit(amount);

        _context.Accounts.Update(account);

        var operation = new Operation(
            OperationId.Default,
            account.Id,
            OperationType.Deposit,
            amount,
            DateTime.UtcNow,
            account.Balance);

        _context.Operations.Add(operation);

        return new Deposit.Response(account.MapToDto());
    }

    public Withdraw.Response Withdraw(Withdraw.Request request)
    {
        Account account = GetAccountForSession(request.SessionId);

        var amount = new Money(request.Amount);

        if (account.Balance < amount)
            return new Withdraw.Response.Failure("Insufficient funds");

        account.Withdraw(amount);

        _context.Accounts.Update(account);

        var operation = new Operation(
            OperationId.Default,
            account.Id,
            OperationType.Withdraw,
            amount,
            DateTime.UtcNow,
            account.Balance);

        _context.Operations.Add(operation);

        return new Withdraw.Response.Success(account.MapToDto());
    }

    public GetHistory.Response GetHistory(GetHistory.Request request)
    {
        Account account = GetAccountForSession(request.SessionId);

        var query = new OperationQuery
        {
            AccountIds = [account.Id],
        };

        OperationDto[] operations = _context.Operations
            .Query(query)
            .Select(o => o.MapToDto())
            .ToArray();

        return new GetHistory.Response(operations);
    }

    private Account GetAccountForSession(Guid sessionId)
    {
        Session session = _context.Sessions.GetSessionId(new SessionId(sessionId))
                          ?? throw new InvalidOperationException("Session not found");

        if (session.Type != SessionType.User || session.AccountId is null)
            throw new InvalidOperationException("Session is not a user session");

        Account account = _context.Accounts.GetById(session.AccountId.Value)
                          ?? throw new InvalidOperationException("Account not found");

        return account;
    }
}
