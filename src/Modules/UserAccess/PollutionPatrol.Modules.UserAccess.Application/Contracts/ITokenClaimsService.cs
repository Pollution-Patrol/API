namespace PollutionPatrol.Modules.UserAccess.Application.Contracts;

public interface ITokenClaimsService
{
    string GenerateToken(ApplicationUser user);
}