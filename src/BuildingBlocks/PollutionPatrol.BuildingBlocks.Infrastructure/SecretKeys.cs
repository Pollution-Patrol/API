namespace PollutionPatrol.BuildingBlocks.Infrastructure;

/// <summary>
/// This class defines the names of the keys used to store secrets in the .NET Secret Manager tool.
/// These keys are used by the UserAccess module to retrieve secrets such as JWT secrets, connection strings, etc.
/// </summary>
public sealed class SecretKeys
{
    public const string SmtpPassword = "email.password";
    public const string EmailSecretKey = "email.username";
    
    public const string JwtSecretSecretKey = "jwtSecret";
}