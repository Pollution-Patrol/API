namespace PollutionPatrol.Modules.Pollution.Application.Models;

public record ReportDto(
    Guid Id,
    string PollutionType,
    string ReportStatus,
    DateTime ReportDate,
    double Longitude,
    double Latitude,
    string? EvidenceFileKey = null,
    Guid? DesignatedReviewerId = null);