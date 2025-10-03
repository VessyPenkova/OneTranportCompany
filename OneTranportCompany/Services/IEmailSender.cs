using System.Threading.Tasks;

namespace OneTransportCompany.Services;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string htmlBody, string? replyTo = null);
    Task SendPlainAsync(string to, string subject, string textBody, string? replyTo = null);
}
