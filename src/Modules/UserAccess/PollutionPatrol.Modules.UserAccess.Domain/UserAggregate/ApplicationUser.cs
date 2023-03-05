namespace PollutionPatrol.Modules.UserAccess.Domain.UserAggregate;

public sealed class ApplicationUser : Entity, IAggregateRoot
{
    private ApplicationUser(string email, string passwordHash, string salt, UserRole role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        Roles = new List<UserRole> { role };
    }

    private ApplicationUser() { }
    
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Salt { get; private set; }

    public List<UserRole> Roles { get; private set; }

    internal static ApplicationUser CreateFromRegistration(Registration registration) =>
        new(registration.Email, registration.PasswordHash, registration.Salt, UserRole.User);

    public static ApplicationUser CreateAdmin(string email, string passwordHash, string salt) =>
        new(email, passwordHash, salt, UserRole.Admin);
}