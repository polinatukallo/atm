using Lab5.Application.Contracts.Accounts.Operations;

namespace Lab5.Application.Contracts.Sessions;

public interface ISessionService
{
    CreateUserSession.Response CreateUserSession(CreateUserSession.Request request);

    CreateAdminSession.Response CreateAdminSession(CreateAdminSession.Request request);
}