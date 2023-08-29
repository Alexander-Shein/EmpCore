using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace EmpCore.Api.Middleware.Security;

public class PrincipalUser : IPrincipalUser
{
    private const string PreferredUsernameClaim = "preferred_username";
    
    private readonly IPrincipal _user;
    private readonly IEnumerable<Claim> _claims;

    public PrincipalUser(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor == null) throw new ArgumentNullException(nameof(httpContextAccessor));
        if (httpContextAccessor.HttpContext == null)
            throw new InvalidOperationException($"{nameof(httpContextAccessor.HttpContext)} is null");

        var user = httpContextAccessor.HttpContext?.User
            ?? throw new InvalidOperationException("Current user isn't available");

        _user = user;
        _claims = user.Claims;
    }

    public string Id
    {
        get
        {
            return _claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
        }
    }

    public string Email
    {
        get
        {
            return _claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)
                ?.Value;
        }
    }

    public string PreferredUserName
    {
        get
        {
            return _claims
                .FirstOrDefault(c => c.Type == PreferredUsernameClaim)
                ?.Value;
        }
    }

    public IReadOnlyList<Claim> Claims => _claims.ToList();

    public bool IsAuthenticated => _user.Identity.IsAuthenticated;
}
