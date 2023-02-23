namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Security;

internal sealed class PasswordManager : IPasswordManager
{
    private const int Length = 16;
    
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

    public byte[] GenerateSalt()
    {
        var salt = new byte[Length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
}