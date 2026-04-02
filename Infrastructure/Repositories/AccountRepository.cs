using Lab5.Application.Abstractions.Persistence.Queries;
using Lab5.Application.Abstractions.Persistence.Repositories;
using Lab5.Domain.Accounts;

namespace Lab5.Infrastructure.Persistence.Repositories;

public sealed class AccountRepository : IAccountRepository
{
    private readonly Dictionary<AccountId, Account> _values = new();

    public Account Add(Account account)
    {
        var newId = new AccountId(_values.Count + 1);

        account = new Account(
            newId,
            account.PinCode,
            account.Balance);

        _values.Add(account.Id, account);
        return account;
    }

    public void Update(Account account)
    {
        if (_values.ContainsKey(account.Id) is false)
            throw new InvalidOperationException("Account not found");

        _values[account.Id] = account;
    }

    public IEnumerable<Account> Query(AccountQuery query)
    {
        IEnumerable<Account> result = _values.Values;

        if (query.Ids is not null)
            result = result.Where(x => query.Ids.Contains(x.Id));

        return result;
    }

    public Account? GetById(AccountId id)
    {
        return _values.GetValueOrDefault(id);
    }
}
