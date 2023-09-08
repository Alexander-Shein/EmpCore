using EmpCore.Domain;

namespace EmpCore.Application.Failures;

public class ResourceNotFoundFailure : Failure
{
    private const string ErrorCode = "resource_not_found";
    private const string ErrorMessage = "Resource not found.";

    public static readonly ResourceNotFoundFailure Instance = new();

    private ResourceNotFoundFailure() : base(ErrorCode, ErrorMessage) { }
}