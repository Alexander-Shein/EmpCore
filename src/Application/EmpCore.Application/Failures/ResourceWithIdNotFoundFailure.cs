using EmpCore.Domain;

namespace EmpCore.Application.Failures;

public class ResourceWithIdNotFoundFailure : Failure
{
    private const string ErrorCode = "resource_not_found";

    public object Id { get; }

    private ResourceWithIdNotFoundFailure(object id)
        : base(ErrorCode, $"Resource with id: '{id}' not found.")
    {
        Id = id;
    }
}