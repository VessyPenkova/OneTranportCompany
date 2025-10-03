using System.ComponentModel.DataAnnotations;

namespace OneTransportCompany.Models;

public class Testimonial
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, StringLength(100)]
    public string ClientName { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Content { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
