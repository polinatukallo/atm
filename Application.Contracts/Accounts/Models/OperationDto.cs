namespace Lab5.Application.Contracts.Accounts.Models;

public sealed record OperationDto(
    long Id,
    string Type,
    decimal Amount,
    DateTime Timestamp,
    decimal BalanceAfter);