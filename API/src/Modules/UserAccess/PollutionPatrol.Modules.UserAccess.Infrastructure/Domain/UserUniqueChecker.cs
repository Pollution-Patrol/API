namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Domain;

internal sealed class UserUniqueChecker : IUserUniqueChecker
{
    private readonly IUserAccessDbContext _dbContext;

    public UserUniqueChecker(IUserAccessDbContext dbContext) => _dbContext = dbContext;

    public bool IsEmailUnique(string email) => _dbContext.Users
        .Any(x => x.Email.Equals(email)) is false;
}