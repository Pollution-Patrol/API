namespace PollutionPatrol.Modules.UserAccess.Application.Contracts;

public interface IPasswordManager
{
    Task<byte[]> HashPasswordAsync(string password, byte[] salt);
    Task<bool> VerifyPasswordAsync(string password, byte[] hash, byte[] salt);
    byte[] GenerateSalt();
}