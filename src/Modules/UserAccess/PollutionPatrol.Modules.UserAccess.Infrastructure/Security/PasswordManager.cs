namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Security;

internal sealed class PasswordManager : IPasswordManager
{
    private const int Length = 16;
    
    /// <summary>
    /// Hashes a password using Argon2id.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <param name="salt">The salt to use when hashing.</param>
    /// <returns>The hashed password.</returns>
    public async Task<byte[]> HashPasswordAsync(string password, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException($"{nameof(password)} cannot be null");

        if (salt is null)
            throw new ArgumentNullException($"{nameof(salt)} cannot be null");

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

        argon2.Salt = salt;
        argon2.DegreeOfParallelism = 8;
        argon2.Iterations = 4;
        argon2.MemorySize = 1024 * 1024;

        return await argon2.GetBytesAsync(Length);
    }

    /// <summary>
    /// Verifies a password against a hashed password and salt.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="hash">The hashed password to verify against.</param>
    /// <param name="salt">The salt used when hashing the password.</param>
    /// <returns>True if the password is verified, false otherwise.</returns>
    public async Task<bool> VerifyPasswordAsync(string password, byte[] hash, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException($"{nameof(password)} cannot be null");

        if (hash is null)
            throw new ArgumentNullException($"{nameof(salt)} cannot be null");

        if (salt is null)
            throw new ArgumentNullException($"{nameof(hash)} cannot be null");

        var newHash = await HashPasswordAsync(password, salt);
        return hash.SequenceEqual(newHash);
    }

    /// <summary>
    /// Generates a random salt.
    /// </summary>
    /// <returns>The generated salt.</returns>
    public byte[] GenerateSalt()
    {
        var salt = new byte[Length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
}