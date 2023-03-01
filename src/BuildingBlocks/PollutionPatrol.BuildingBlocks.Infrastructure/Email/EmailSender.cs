namespace PollutionPatrol.BuildingBlocks.Infrastructure.Email;

internal sealed class EmailSender : IEmailSender
{
    private readonly EmailOptions _emailOptions;

    public EmailSender(IOptions<EmailOptions> options) => _emailOptions = options.Value;

    public async Task SendAsync(EmailMessage message)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Pollution Patrol", _emailOptions.Username));
        message.To.ForEach(x => email.To.Add(new MailboxAddress(default, x)));
        email.Subject = message.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message.Content
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_emailOptions.Host, _emailOptions.Port, false);
        await smtp.AuthenticateAsync(_emailOptions.Username, _emailOptions.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}