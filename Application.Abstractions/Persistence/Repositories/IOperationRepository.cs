using Lab5.Application.Abstractions.Persistence.Queries;
using Lab5.Domain.Operations;

namespace Lab5.Application.Abstractions.Persistence.Repositories;

public interface IOperationRepository
{
    Operation Add(Operation operation);

    IEnumerable<Operation> Query(OperationQuery query);
}