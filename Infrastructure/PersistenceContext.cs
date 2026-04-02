using Lab5.Application.Abstractions.Persistence;
using Lab5.Application.Abstractions.Persistence.Repositories;

namespace Lab5.Infrastructure.Persistence;

internal sealed class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(
        IAccountRepository accounts,
        ISessionRepository sessions,
        IOperationRepository operations)
    {
        Accounts = accounts;
        Sessions = sessions;
        Operations = operations;
    }

    public IAccountRepository Accounts { get; }

    public ISessionRepository Sessions { get; }

    public IOperationRepository Operations { get; }
}