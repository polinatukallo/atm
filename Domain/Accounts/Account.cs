using Lab5.Domain.ValueObjects;

namespace Lab5.Domain.Accounts;

public sealed class Account
{
    public Account(AccountId id, PinCode pinCode, Money balance)
    {
        Id = id;
        PinCode = pinCode;
        Balance = balance;
    }

    public AccountId Id { get; }

    public PinCode PinCode { get; private set; }

    public Money Balance { get; private set; }

    public void Deposit(Money amount)
    {
        Balance += amount;
    }

    public void Withdraw(Money amount)
    {
        if (Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        Balance = Balance - amount;
    }
}