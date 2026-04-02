using Lab5.Domain.Accounts;
using Lab5.Domain.ValueObjects;

namespace Lab5.Domain.Operations;

public sealed class Operation
{
    public Operation(
        OperationId id,
        AccountId accountId,
        OperationType type,
        Money amount,
        DateTime timestamp,
        Money balanceAfter)
    {
        Id = id;
        AccountId = accountId;
        Type = type;
        Amount = amount;
        Timestamp = timestamp;
        BalanceAfter = balanceAfter;
    }

    public OperationId Id { get; }

    public AccountId AccountId { get; }

    public OperationType Type { get; }

    public Money Amount { get; }

    public DateTime Timestamp { get; }

    public Money BalanceAfter { get; }
}