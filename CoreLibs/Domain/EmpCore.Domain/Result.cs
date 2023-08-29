namespace EmpCore.Domain;

public class Result
{
    private const string FailureResultRequiresErrorMessage = "A failure result requires at least one error.";
    private const string NullCollectionElementMessage = "Collection element is null.";

    private static readonly Result SuccessInstance = new();

    protected internal Result() { }

    protected internal Result(params Failure[] failures)
    {
        if (failures == null) throw new ArgumentNullException(nameof(failures));
        if (!failures.Any()) throw new InvalidOperationException(FailureResultRequiresErrorMessage);

        foreach (var failure in failures)
        {
            if (failure == null) throw new ArgumentNullException(nameof(failures), NullCollectionElementMessage);
            _failures.Add(failure);
        }
    }

    public IReadOnlyList<Failure> Failures => _failures.ToList();
    private readonly List<Failure> _failures = new();

    public bool IsFailure => _failures.Any();
    public bool IsSuccess => !IsFailure;


    public static Result Combine(params Result[] results)
    {
        var errors = new List<Failure>();

        foreach (var result in results)
        {
            if (result == null) throw new ArgumentNullException(nameof(results), NullCollectionElementMessage);
            if (result.IsFailure) errors.AddRange(result.Failures);
        }

        if (!errors.Any()) return SuccessInstance;
        return new Result(errors.ToArray());
    }


    public static Result Success() => SuccessInstance;
    public static Result<T> Success<T>(T value) => new(value);


    public static Result Failure(Failure failure) => new(failure);
    public static Result Failure(IEnumerable<Failure> failures) => new(failures?.ToArray() ?? Array.Empty<Failure>());
    public static Result<T> Failure<T>(Failure failure) => new(failure);
    public static Result<T> Failure<T>(IEnumerable<Failure> failures) => new(failures?.ToArray() ?? Array.Empty<Failure>());
}

public class Result<T> : Result
{
    private const string AccessValueInFailureResultMessage = "You attempted to access the Value property for a failure result. A failure result has no Value.";

    private readonly T _value;

    public Result(T value)
    {
        _value = value;
    }

    protected internal Result(params Failure[] failures) : base(failures)
    {
    }

    public T Value
    {
        get
        {
            if (IsFailure) throw new InvalidOperationException(AccessValueInFailureResultMessage);

            return _value;
        }
    }
    
    public static implicit operator T(Result<T> result) => result == null ? default : result.Value;
    public static implicit operator Result<T>(T value) => Success(value);
        
    public static implicit operator Result<T>(List<Failure> failures) => Failure<T>(failures);
    public static implicit operator Result<T>(Failure[] failures) => Failure<T>(failures);
}
