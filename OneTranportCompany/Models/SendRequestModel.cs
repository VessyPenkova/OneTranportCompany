using System.ComponentModel.DataAnnotations;

namespace OneTransportCompany.Models;

public class SendRequestModel
{
    [Required, StringLength(100)]
    public string CompanyName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string ContactEmail { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string PickupAddress { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Required, Range(0.1, 100000)]
    public double WeightKg { get; set; }

    [StringLength(300)]
    public string? Dimensions { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime PickupDate { get; set; } = DateTime.Today.AddDays(1);

    [StringLength(1000)]
    public string? Notes { get; set; }
}
