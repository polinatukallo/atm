using Lab5.Domain.Accounts;

namespace Lab5.Application.Abstractions.Persistence.Queries;

public sealed class AccountQuery
{
    public IReadOnlyCollection<AccountId>? Ids { get; init; }
}