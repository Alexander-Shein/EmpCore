namespace EmpCore.Domain;

[Serializable]
public abstract class SingleValueObject<T> : ValueObject
    where T : IComparable
{
    public T Value { get; }

    protected SingleValueObject(T value)
    {
        Value = value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value?.ToString();
    }

    public static implicit operator T(SingleValueObject<T> valueObject)
    {
        return valueObject == null ? default : valueObject.Value;
    }
}
