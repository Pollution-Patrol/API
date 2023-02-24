namespace PollutionPatrol.BuildingBlocks.Application.Email;

public interface IEmailSender
{
    Task SendAsync(EmailMessage message);
}