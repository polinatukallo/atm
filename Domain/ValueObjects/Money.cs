namespace Lab5.Domain.ValueObjects;

public sealed record Money
{
    public Money(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Money must be non-negative", nameof(value));
        }

        Value = value;
    }

    public decimal Value { get; }

    public static Money operator +(Money left, Money right) => new(left.Value + right.Value);

    public static Money operator -(Money left, Money right)
    {
        if (left.Value < right.Value)
        {
            throw new InvalidOperationException("Resulting money is negative");
        }

        return new Money(left.Value - right.Value);
    }

    public static bool operator <(Money left, Money right) => left.Value < right.Value;

    public static bool operator >(Money left, Money right) => left.Value > right.Value;
}