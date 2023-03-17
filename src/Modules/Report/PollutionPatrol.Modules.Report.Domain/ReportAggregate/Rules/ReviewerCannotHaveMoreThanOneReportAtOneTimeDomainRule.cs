namespace PollutionPatrol.Modules.Report.Domain.ReportAggregate.Rules;

internal sealed class ReviewerCannotHaveMoreThanOneReportAtOneTimeDomainRule : IDomainRule
{
    private readonly IReviewerAssignment _reviewerAssignment;
    private readonly Guid _reviewerId;

    internal ReviewerCannotHaveMoreThanOneReportAtOneTimeDomainRule(IReviewerAssignment reviewerAssignment, Guid reviewerId)
    {
        _reviewerAssignment = reviewerAssignment;
        _reviewerId = reviewerId;
    }

    public string Message => "A reviewer can only review one pollution report at a time.";

    public bool IsBroken() => !_reviewerAssignment.IsReviewerAvailable(_reviewerId);
}