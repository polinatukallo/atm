using Lab5.Application.Contracts.Accounts.Models;

namespace Lab5.Application.Contracts.Accounts.Operations;

public static class Deposit
{
    public readonly record struct Request(Guid SessionId, decimal Amount);

    public readonly record struct Response(AccountDto Account);
}