namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Security;

internal sealed class TokenClaimsService : ITokenClaimsService
{
    private readonly IConfiguration _config;

    public TokenClaimsService(IConfiguration config) => _config = config;

    public string GenerateToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config[SecretKeys.JwtSecretSecretKey]); // JwtSecretKey from .NET secrets manager  

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email)
        };

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