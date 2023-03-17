namespace PollutionPatrol.Modules.Report.Domain.ReportAggregate.Rules;

internal sealed class ReportCannotBeCompletedIfEvidenceFileKeyHasNotBeenSetDomainRule : IDomainRule
{
    private readonly string? _evidenceFileKey;

    internal ReportCannotBeCompletedIfEvidenceFileKeyHasNotBeenSetDomainRule(string? evidenceFileKey) => _evidenceFileKey = evidenceFileKey;

    public string Message => "The report cannot be completed without an evidence file.";

    public bool IsBroken() => string.IsNullOrWhiteSpace(_evidenceFileKey);
}