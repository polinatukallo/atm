using Lab5.Application.Contracts.Accounts.Operations;

namespace Lab5.Application.Contracts.Accounts;

public interface IAccountService
{
    CreateAccount.Response CreateAccount(CreateAccount.Request request);

    GetBalance.Response GetBalance(GetBalance.Request request);

    Deposit.Response Deposit(Deposit.Request request);

    Withdraw.Response Withdraw(Withdraw.Request request);

    GetHistory.Response GetHistory(GetHistory.Request request);
}