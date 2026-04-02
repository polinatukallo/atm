using Lab5.Application.Contracts.Accounts.Models;
using Lab5.Domain.Operations;

namespace Lab5.Application.Mapping;

public static class OperationMappingExtensions
{
    public static OperationDto MapToDto(this Operation operation)
    {
        return new OperationDto(
            operation.Id.Value,
            operation.Type.ToString(),
            operation.Amount.Value,
            operation.Timestamp,
            operation.BalanceAfter.Value);
    }
}