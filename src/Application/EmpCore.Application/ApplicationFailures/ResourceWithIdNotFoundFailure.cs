using EmpCore.Domain;

namespace EmpCore.Application.ApplicationFailures;

public class ResourceWithIdNotFoundFailure : Failure
{
    private const string ErrorCode = "resource_not_found";
    private const string ErrorMessage = "Resource not found.";

    public object Id { get; }

    private ResourceWithIdNotFoundFailure(object id)
        : base(ErrorCode, $"Resource with id: '{id}' not found.")
    {
        Id = id;
    }
}