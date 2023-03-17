namespace PollutionPatrol.Modules.Pollution.Domain.ReportAggregate.Rules;

internal sealed class ReportReviewerCanBeChangedDuringPendingStatusOnlyDomainRule : IDomainRule
{
    private readonly ReportStatus _status;

    internal ReportReviewerCanBeChangedDuringPendingStatusOnlyDomainRule(ReportStatus status) => _status = status;

    public string Message => "The report reviewer can only be changed during the pending status.";

    public bool IsBroken() => !_status.Equals(ReportStatus.Pending);
}