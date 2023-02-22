namespace PollutionPatrol.Modules.UserAccess.Application.Contracts;

public interface IUserAccessDbContext : IDbContext
{
    DbSet<Registration> Registrations { get; }
    DbSet<ApplicationUser> Users { get; }
}