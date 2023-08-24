using System.Security.Claims;

namespace EmpCore.Api.Middleware.Security;

public interface IPrincipalUser
{
    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    public IReadOnlyList<Claim> Claims { get; }
    public bool IsAuthenticated { get; }
}
