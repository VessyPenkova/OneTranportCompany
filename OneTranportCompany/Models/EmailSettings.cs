namespace OneTransportCompany.Models;

public class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public bool EnableSsl { get; set; } = true;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromName { get; set; } = "One Transport Company";
    /// <summary>
    /// Default recipient for inbound messages to the company (ops desk).
    /// </summary>
    public string DefaultTo { get; set; } = string.Empty;
}
