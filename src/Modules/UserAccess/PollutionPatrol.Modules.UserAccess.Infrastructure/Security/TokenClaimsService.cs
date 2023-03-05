using ClaimTypes = PollutionPatrol.Modules.UserAccess.Infrastructure.Security.ClaimTypes;

namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Security;

internal sealed class TokenClaimsService : ITokenClaimsService
{
    private readonly SecurityOptions _securityOptions;

    public TokenClaimsService(IOptions<SecurityOptions> options) => _securityOptions = options.Value;

    public string GenerateToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_securityOptions.JwtSecretKey);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        
        foreach (var role in user.Roles)
            claims.Add(new Claim(System.Security.Claims.ClaimTypes.Role, role.Value));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}