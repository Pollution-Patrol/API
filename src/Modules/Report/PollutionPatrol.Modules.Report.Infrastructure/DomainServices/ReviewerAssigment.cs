namespace PollutionPatrol.Modules.Report.Infrastructure.DomainServices;

internal sealed class ReviewerAssignment : IReviewerAssignment
{
    private readonly IReportDbContext _dbContext;

    public ReviewerAssignment(IReportDbContext dbContext) => _dbContext = dbContext;

    public bool IsReviewerAvailable(Guid reviewerId) =>
        !_dbContext.Reports.Any(x => x.DesignatedReviewerId.Equals(reviewerId) && x.Status.Equals(ReportStatus.Reviewing));
}