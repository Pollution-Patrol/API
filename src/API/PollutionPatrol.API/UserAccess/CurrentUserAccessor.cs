using PollutionPatrol.BuildingBlocks.Application.UserAccess;
using ClaimTypes = PollutionPatrol.Modules.UserAccess.Infrastructure.Security.ClaimTypes;

namespace PollutionPatrol.API.UserAccess;

internal sealed class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public Guid Id
    {
        get
        {
            var idString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Id)?.Value;

            if (string.IsNullOrWhiteSpace(idString))
                throw new AuthenticationException();

            Guid.TryParse(idString, out Guid id);

            return id;
        }
    }

    public string Email
    {
        get
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                throw new AuthenticationException();

            return email;
        }
    }
}