namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Security;

/// <summary>
/// This class defines the names of the keys used to store secrets in the .NET Secret Manager tool.
/// These keys are used by the UserAccess module to retrieve secrets such as JWT secrets, connection strings, etc.
/// </summary>
internal sealed class SecretKeys
{
    public const string JwtSecretSecretKey = "Modules.UserAccess.JwtSecret";
}