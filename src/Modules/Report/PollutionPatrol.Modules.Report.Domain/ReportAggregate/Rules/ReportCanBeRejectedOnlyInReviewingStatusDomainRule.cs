namespace PollutionPatrol.Modules.Report.Domain.ReportAggregate.Rules;

internal sealed class ReportCanBeRejectedOnlyInReviewingStatusDomainRule : IDomainRule
{
    private readonly ReportStatus _status;

    internal ReportCanBeRejectedOnlyInReviewingStatusDomainRule(ReportStatus status) => _status = status;

    public string Message => "A report can only be rejected while it is in the Reviewing status.";

    public bool IsBroken() => !_status.Equals(ReportStatus.Reviewing);
}