namespace PollutionPatrol.Modules.UserAccess.Application.Features.UserRegistration;

internal sealed class SendUserRegistrationConfirmationEmailCommand : IDomainEventHandler<NewRegistrationCreatedDomainEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ApiOptions _apiOptions;

    public SendUserRegistrationConfirmationEmailCommand(IEmailSender emailSender, IOptions<ApiOptions> options)
    {
        _emailSender = emailSender;
        _apiOptions = options.Value;
    }

    public async Task Handle(NewRegistrationCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var link = $"<a href=\"{_apiOptions.BaseUri}/api/registration/confirm?confirmationToken={domainEvent.ConfirmationToken}\">Confirm</a>";
        var builder = new StringBuilder("<html><body>");
        builder.Append("<p>Hey there!</p>");
        builder.Append("<p>Thanks for signing up for Pollution Patrol! To complete your registration, please click this link:</p>");
        builder.Append($"<p>{link}</p>");
        builder.Append("<p>If you have any questions or issues, please reach out to us anytime.</p>");
        builder.Append("<p>Thanks again for joining us!</p>");
        builder.Append("</body></html>");

        var emailMessage = new EmailMessage(
            To: new List<string> { domainEvent.UserEmail },
            Subject: "Pollution Patrol - Please confirm your registration",
            Content: String.Concat(builder)
        );

        await _emailSender.SendAsync(emailMessage);
    }
}