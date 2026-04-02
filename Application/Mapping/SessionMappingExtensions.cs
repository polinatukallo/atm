using Lab5.Application.Contracts.Accounts.Models;
using Lab5.Domain.Sessions;

namespace Lab5.Application.Mapping;

public static class SessionMappingExtensions
{
    public static SessionDto MapToDto(this Session session)
    {
        return new SessionDto(session.Id.Value, session.Type.ToString());
    }
}