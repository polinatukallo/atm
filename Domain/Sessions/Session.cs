using Lab5.Domain.Accounts;

namespace Lab5.Domain.Sessions;

public sealed class Session
{
    public Session(SessionId id, SessionType type, AccountId? accountId)
    {
        Id = id;
        Type = type;
        AccountId = accountId;
    }

    public SessionId Id { get; }

    public SessionType Type { get; }

    public AccountId? AccountId { get; }
}