namespace PollutionPatrol.Modules.Pollution.Application.Services;

public static class ReportStatusService
{
    internal static ReportStatus Parse(string reportStatusString)
    {
        IReadOnlyDictionary<string, ReportStatus> statuses = GetAllReportStatuses();
        return statuses[reportStatusString];
    }

    internal static bool IsDefined(string? statusString)
    {
        if (string.IsNullOrWhiteSpace(statusString))
            return false;
        
        IReadOnlyDictionary<string, ReportStatus> statuses = GetAllReportStatuses();

        return statuses.ContainsKey(statusString);
    }
    
    private static IReadOnlyDictionary<string, ReportStatus> GetAllReportStatuses()
    {
        Type type = typeof(ReportStatus);
        IReadOnlyDictionary<string, ReportStatus> reportStatuses = type.GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(ReportStatus))
            .ToDictionary(p => p.Name, f => (ReportStatus)f.GetValue(null));

        return reportStatuses;
    }
}