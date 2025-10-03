using System.ComponentModel.DataAnnotations;

namespace OneTransportCompany.Models;

public class ContactFormModel
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    public string? Phone { get; set; }

    [Required, StringLength(1000, MinimumLength = 10)]
    public string Message { get; set; } = string.Empty;
}
