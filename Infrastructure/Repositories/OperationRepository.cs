using Lab5.Application.Abstractions.Persistence.Queries;
using Lab5.Application.Abstractions.Persistence.Repositories;
using Lab5.Domain.Operations;

namespace Lab5.Infrastructure.Persistence.Repositories;

public sealed class OperationRepository : IOperationRepository
{
    private readonly List<Operation> _values = [];

    public Operation Add(Operation operation)
    {
        var newId = new OperationId(_values.Count + 1);

        operation = new Operation(
            newId,
            operation.AccountId,
            operation.Type,
            operation.Amount,
            operation.Timestamp,
            operation.BalanceAfter);

        _values.Add(operation);
        return operation;
    }

    public IEnumerable<Operation> Query(OperationQuery query)
    {
        IEnumerable<Operation> result = _values;

        if (query.AccountIds is not null)
            result = result.Where(x => query.AccountIds.Contains(x.AccountId));

        return result;
    }
}