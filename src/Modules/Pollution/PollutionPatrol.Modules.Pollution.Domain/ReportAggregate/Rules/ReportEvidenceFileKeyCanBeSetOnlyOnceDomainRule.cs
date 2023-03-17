namespace PollutionPatrol.Modules.Pollution.Domain.ReportAggregate.Rules;

internal sealed class ReportEvidenceFileKeyCanBeSetOnlyOnceDomainRule : IDomainRule
{
    private readonly string? _evidenceFileKey;

    internal ReportEvidenceFileKeyCanBeSetOnlyOnceDomainRule(string? evidenceFileKey) => _evidenceFileKey = evidenceFileKey;

    public string Message => "Report evidence file can be set only once to complete report.";

    public bool IsBroken() => !string.IsNullOrWhiteSpace(_evidenceFileKey);
}