using Lab5.Application.Abstractions.Persistence.Repositories;
using Lab5.Domain.Sessions;

namespace Lab5.Infrastructure.Persistence.Repositories;

public sealed class SessionRepository : ISessionRepository
{
    private readonly Dictionary<SessionId, Session> _values = new();

    public Session Add(Session session)
    {
        _values.Add(session.Id, session);
        return session;
    }

    public Session? GetSessionId(SessionId id)
    {
        return _values.GetValueOrDefault(id);
    }
}