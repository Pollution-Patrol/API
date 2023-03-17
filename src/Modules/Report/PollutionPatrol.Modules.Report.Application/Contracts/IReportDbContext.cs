namespace PollutionPatrol.Modules.Report.Application.Contracts;

public interface IReportDbContext : IDbContext
{
    DbSet<Domain.ReportAggregate.Report> Reports { get; }
}