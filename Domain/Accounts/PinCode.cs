namespace Lab5.Domain.Accounts;

public sealed record PinCode
{
    public PinCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("PIN is required", nameof(value));
        }

        if (value.Length != 4 || value.Any(c => c is < '0' or > '9'))
        {
            throw new ArgumentException("PIN must be 4 digits", nameof(value));
        }

        Value = value;
    }

    public string Value { get; }
}
