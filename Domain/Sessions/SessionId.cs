namespace Lab5.Domain.Sessions;

public readonly record struct SessionId(Guid Value)
{
    public static SessionId New()
    {
        return new SessionId(Guid.NewGuid());
    }
}
