namespace PollutionPatrol.API.Models;

public record ReportPollutionRequest(string PollutionType, double Longitude, double Latitude);