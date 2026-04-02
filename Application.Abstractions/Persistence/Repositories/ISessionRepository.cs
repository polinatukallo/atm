using Lab5.Domain.Sessions;

namespace Lab5.Application.Abstractions.Persistence.Repositories;

public interface ISessionRepository
{
    Session Add(Session session);

    Session? GetSessionId(SessionId id);
}