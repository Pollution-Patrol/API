namespace PollutionPatrol.BuildingBlocks.Application.Email;

public record EmailMessage(List<string> To, string Subject, string Content);