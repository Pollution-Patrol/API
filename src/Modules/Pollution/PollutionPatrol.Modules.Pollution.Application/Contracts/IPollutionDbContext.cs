namespace PollutionPatrol.Modules.Pollution.Application.Contracts;

public interface IPollutionDbContext : IDbContext
{
    DbSet<Report> Reports { get; }
}