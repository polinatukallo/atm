namespace Lab5.Domain.Operations;

public readonly record struct OperationId(long Value)
{
    public static readonly OperationId Default = new(0);
}
