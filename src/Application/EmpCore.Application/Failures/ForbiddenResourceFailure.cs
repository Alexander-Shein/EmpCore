using EmpCore.Domain;

namespace EmpCore.Application.Failures;

public class ForbiddenResourceFailure : Failure
{
    private const string ErrorCode = "access_forbidden";
    
    public object Id { get; }

    private ForbiddenResourceFailure(object id)
        : base(ErrorCode, $"Access for resource with id: '{id}' is forbidden.")
    {
        Id = id;
    }
}