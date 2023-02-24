namespace PollutionPatrol.BuildingBlocks.Infrastructure.Email;

internal sealed class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(EmailMessage message)
    {
        var username = _config[SecretKeys.EmailSecretKey];
        var password = _config[SecretKeys.SmtpPassword];

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Pollution Patrol", username));
        message.To.ForEach(x => email.To.Add(new MailboxAddress(default, x)));
        email.Subject = message.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message.Content
        };
        
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, false);
        await smtp.AuthenticateAsync(username, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}