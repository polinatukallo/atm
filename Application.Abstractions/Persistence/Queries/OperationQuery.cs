using Lab5.Domain.Accounts;

namespace Lab5.Application.Abstractions.Persistence.Queries;

public sealed class OperationQuery
{
    public IReadOnlyCollection<AccountId>? AccountIds { get; init; }
}