using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace EmpCore.Api.Middleware.Security;

public class PrincipalUser : IPrincipalUser
{
    private const string FirstNameClaimType = "first_name";
    private const string LastNameClaimType = "last_name";

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

    public Guid Id
    {
        get
        {
            var userId = _claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            
            return Guid.Parse(userId);
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

    public string FirstName
    {
        get
        {
            return _claims
                .FirstOrDefault(c => c.Type == FirstNameClaimType)
                ?.Value;
        }
    }

    public string LastName
    {
        get
        {
            return _claims
                .FirstOrDefault(c => c.Type == LastNameClaimType)
                ?.Value;
        }
    }

    public IReadOnlyList<Claim> Claims => _claims.ToList();

    public bool IsAuthenticated => _user.Identity.IsAuthenticated;
}
