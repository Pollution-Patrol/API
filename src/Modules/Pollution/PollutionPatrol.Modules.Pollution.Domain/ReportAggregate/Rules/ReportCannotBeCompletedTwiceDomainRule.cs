namespace PollutionPatrol.Modules.Pollution.Domain.ReportAggregate.Rules;

public class ReportCannotBeCompletedTwiceDomainRule : IDomainRule
{
    private readonly ReportStatus _status;

    internal ReportCannotBeCompletedTwiceDomainRule(ReportStatus status) => _status = status;

    public string Message => "The report has already been completed and cannot be completed again.";

    public bool IsBroken() => _status.Equals(ReportStatus.Pending)
                              || _status.Equals(ReportStatus.Approved)
                              || _status.Equals(ReportStatus.Rejected);
}