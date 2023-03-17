namespace PollutionPatrol.Modules.Pollution.Domain.ReportAggregate.Events;

public sealed record ReportPollutionEvidenceFileHasBeenSetDomainEvent(Pollution.Domain.ReportAggregate.Report Report) : IDomainEvent;
