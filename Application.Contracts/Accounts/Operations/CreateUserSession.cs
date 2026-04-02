using Lab5.Application.Contracts.Accounts.Models;

namespace Lab5.Application.Contracts.Accounts.Operations;

public static class CreateUserSession
{
    public readonly record struct Request(long AccountId, string Pin);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(SessionDto Session) : Response;

        public sealed record Failure(string Message) : Response;
    }
}