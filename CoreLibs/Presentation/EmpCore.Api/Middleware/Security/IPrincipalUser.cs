using System.Security.Claims;

namespace EmpCore.Api.Middleware.Security;

public interface IPrincipalUser
{
    public string Id { get; }
    public string PreferredUserName { get; }
    public string Email { get; }

    public IReadOnlyList<Claim> Claims { get; }
    public bool IsAuthenticated { get; }
}
