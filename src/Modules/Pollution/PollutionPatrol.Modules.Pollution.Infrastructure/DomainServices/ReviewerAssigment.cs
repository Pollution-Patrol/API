namespace PollutionPatrol.Modules.Pollution.Infrastructure.DomainServices;

internal sealed class ReviewerAssignment : IReviewerAssignment
{
    private readonly IPollutionDbContext _dbContext;

    public ReviewerAssignment(IPollutionDbContext dbContext) => _dbContext = dbContext;

    public bool IsReviewerAvailable(Guid reviewerId) =>
        !_dbContext.Reports.Any(x => x.DesignatedReviewerId.Equals(reviewerId) && x.Status.Equals(ReportStatus.Reviewing));
}