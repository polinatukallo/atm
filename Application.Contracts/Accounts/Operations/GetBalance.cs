namespace Lab5.Application.Contracts.Accounts.Operations;

public static class GetBalance
{
    public readonly record struct Request(Guid SessionId);

    public readonly record struct Response(decimal Balance);
}