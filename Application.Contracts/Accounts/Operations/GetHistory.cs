using Lab5.Application.Contracts.Accounts.Models;

namespace Lab5.Application.Contracts.Accounts.Operations;

public static class GetHistory
{
    public readonly record struct Request(Guid SessionId);

    public readonly record struct Response(IReadOnlyCollection<OperationDto> Operations);
}