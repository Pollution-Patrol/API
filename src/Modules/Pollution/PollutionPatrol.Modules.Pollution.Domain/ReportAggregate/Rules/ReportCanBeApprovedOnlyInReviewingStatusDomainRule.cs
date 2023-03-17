namespace PollutionPatrol.Modules.Pollution.Domain.ReportAggregate.Rules;

internal sealed class ReportCanBeApprovedOnlyInReviewingStatusDomainRule : IDomainRule
{
    private readonly ReportStatus _status;

    internal ReportCanBeApprovedOnlyInReviewingStatusDomainRule(ReportStatus status) => _status = status;

    public string Message => "A report can only be approved while it is in the Reviewing status.";

    public bool IsBroken() => !_status.Equals(ReportStatus.Reviewing);
}