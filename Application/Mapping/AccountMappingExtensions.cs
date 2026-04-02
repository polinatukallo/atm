using Lab5.Application.Contracts.Accounts.Models;
using Lab5.Domain.Accounts;

namespace Lab5.Application.Mapping;

public static class AccountMappingExtensions
{
    public static AccountDto MapToDto(this Account account)
    {
        return new AccountDto(account.Id.Value, account.Balance.Value);
    }
}