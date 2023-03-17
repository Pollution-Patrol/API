namespace PollutionPatrol.Modules.Report.Domain.ReportAggregate.Events;

public sealed record ReportPollutionEvidenceFileHasBeenSetDomainEvent(Report Report) : IDomainEvent;
