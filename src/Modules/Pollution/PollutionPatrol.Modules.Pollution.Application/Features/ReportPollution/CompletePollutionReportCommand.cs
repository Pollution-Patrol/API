namespace PollutionPatrol.Modules.Pollution.Application.Features.ReportPollution;

internal sealed class CompletePollutionReportCommand : IDomainEventHandler<ReportPollutionEvidenceFileHasBeenSetDomainEvent>
{
    private readonly IPollutionDbContext _dbContext;

    public CompletePollutionReportCommand(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(ReportPollutionEvidenceFileHasBeenSetDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var report = domainEvent.Report;

        report.Complete();
 
        _dbContext.Reports.Update(report);
        await _dbContext.CommitAsync();
    }
}