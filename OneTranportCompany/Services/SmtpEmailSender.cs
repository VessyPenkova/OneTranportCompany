using System.Net;
using System.Net.Mail;
using System.Text;
using OneTransportCompany.Models;

namespace OneTransportCompany.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly EmailSettings settings;

    public SmtpEmailSender(EmailSettings settings)
    {
        this.settings = settings;
    }

    public async Task SendAsync(string to, string subject, string htmlBody, string? replyTo = null)
    {
        using var msg = new MailMessage
        {
            From = new MailAddress(settings.User, settings.FromName, Encoding.UTF8),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true,
            BodyEncoding = Encoding.UTF8,
            SubjectEncoding = Encoding.UTF8
        };
        msg.To.Add(to);
        if (!string.IsNullOrWhiteSpace(replyTo))
            msg.ReplyToList.Add(new MailAddress(replyTo));

        using var client = CreateClient();
        await client.SendMailAsync(msg);
    }

    public async Task SendPlainAsync(string to, string subject, string textBody, string? replyTo = null)
    {
        using var msg = new MailMessage
        {
            From = new MailAddress(settings.User, settings.FromName, Encoding.UTF8),
            Subject = subject,
            Body = textBody,
            IsBodyHtml = false,
            BodyEncoding = Encoding.UTF8,
            SubjectEncoding = Encoding.UTF8
        };
        msg.To.Add(to);
        if (!string.IsNullOrWhiteSpace(replyTo))
            msg.ReplyToList.Add(new MailAddress(replyTo));

        using var client = CreateClient();
        await client.SendMailAsync(msg);
    }

    private SmtpClient CreateClient()
    {
        var client = new SmtpClient(settings.Host, settings.Port)
        {
            EnableSsl = settings.EnableSsl,
            Credentials = new NetworkCredential(settings.User, settings.Password)
        };
        return client;
    }
}
