namespace PollutionPatrol.Modules.Pollution.Application.Features.ReportPollution;

public record ReportDto(Guid Id, string PollutionType, double Longitude, double Latitude, string? EvidenceFileKey = null);