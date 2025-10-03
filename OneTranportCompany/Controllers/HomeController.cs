using Microsoft.AspNetCore.Mvc;
using OneTransportCompany.Models;
using OneTransportCompany.Services;
using System.Text;

namespace OneTransportCompany.Controllers;

public class HomeController : Controller
{
    private readonly IInMemoryStore store;
    private readonly IEmailSender emailSender;
    private readonly EmailSettings emailSettings;

    public HomeController(IInMemoryStore store, IEmailSender emailSender, EmailSettings emailSettings)
    {
        this.store = store;
        this.emailSender = emailSender;
        this.emailSettings = emailSettings;
    }

    public IActionResult Index() => View();

    public IActionResult About() => View();

    [HttpGet]
    public IActionResult Contact() => View(new ContactFormModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactFormModel model)
    {
        if (!ModelState.IsValid) return View(model);

        store.AddContact(model);

        // Email to ops desk
        var subject = $"[Contact] {model.Name}";
        var body = $@"
<h3>New Contact Message</h3>
<p><strong>Name:</strong> {HtmlEncode(model.Name)}</p>
<p><strong>Email:</strong> {HtmlEncode(model.Email)}</p>
<p><strong>Phone:</strong> {HtmlEncode(model.Phone ?? "-")}</p>
<p><strong>Message:</strong><br/>{HtmlEncode(model.Message).Replace("\n", "<br/>")}</p>
<p style='color:#888'>Sent via One Transport Company website</p>";
        await emailSender.SendAsync(emailSettings.DefaultTo, subject, body, replyTo: model.Email);

        // Auto-reply to sender (optional)
        var ackSubject = "Thanks for contacting One Transport Company";
        var ackBody = $@"
<p>Hi {HtmlEncode(model.Name)},</p>
<p>Thanks for reaching out. Our team will get back to you shortly.</p>
<p>— One Transport Company</p>";
        await emailSender.SendAsync(model.Email, ackSubject, ackBody);

        TempData["Success"] = "Thank you! We received your message and will reply shortly.";
        return RedirectToAction(nameof(Contact));
    }

    [HttpGet]
    public IActionResult SendRequest() => View(new SendRequestModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendRequest(SendRequestModel model)
    {
        if (!ModelState.IsValid) return View(model);

        store.AddRequest(model);

        // Email to ops desk with request details
        var subject = $"[Request] {model.CompanyName} → Quote request";
        var body = $@"
<h3>New Transport Request</h3>
<ul>
  <li><strong>Company:</strong> {HtmlEncode(model.CompanyName)}</li>
  <li><strong>Contact Email:</strong> {HtmlEncode(model.ContactEmail)}</li>
  <li><strong>Pickup:</strong> {HtmlEncode(model.PickupAddress)}</li>
  <li><strong>Delivery:</strong> {HtmlEncode(model.DeliveryAddress)}</li>
  <li><strong>Weight (kg):</strong> {model.WeightKg}</li>
  <li><strong>Dimensions:</strong> {HtmlEncode(model.Dimensions ?? "-")}</li>
  <li><strong>Pickup Date:</strong> {model.PickupDate:yyyy-MM-dd}</li>
</ul>
<p><strong>Notes:</strong><br/>{HtmlEncode(model.Notes ?? "-").Replace("\n", "<br/>")}</p>
<p style='color:#888'>Sent via One Transport Company website</p>";
        await emailSender.SendAsync(emailSettings.DefaultTo, subject, body, replyTo: model.ContactEmail);

        // Acknowledgement to requester
        var ackSubject = "We received your transport request";
        var ackBody = $@"
<p>Hello {HtmlEncode(model.CompanyName)},</p>
<p>Thanks for your request. Our operations team is reviewing the details and will send a quote soon.</p>
<p>Pickup: {HtmlEncode(model.PickupAddress)}<br/>
Delivery: {HtmlEncode(model.DeliveryAddress)}<br/>
Pickup Date: {model.PickupDate:yyyy-MM-dd}</p>
<p>— One Transport Company</p>";
        await emailSender.SendAsync(model.ContactEmail, ackSubject, ackBody);

        TempData["Success"] = "Your transport request has been submitted. We’ll get back with a quote.";
        return RedirectToAction(nameof(SendRequest));
    }

    public IActionResult Testimonials()
    {
        var testimonials = store.Testimonials
            .OrderByDescending(t => t.CreatedOn)
            .ToList();
        return View(testimonials);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTestimonial([FromForm] Testimonial model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please provide your name, your review, and a rating.";
            return RedirectToAction(nameof(Testimonials));
        }

        model.CreatedOn = DateTime.UtcNow;
        store.AddTestimonial(model);

        // Notify ops desk of a new testimonial (you can moderate if needed)
        var subject = $"[Testimonial] {model.ClientName} ({model.Rating}/5)";
        var body = $@"
<h3>New Testimonial Submitted</h3>
<p><strong>Client:</strong> {HtmlEncode(model.ClientName)}</p>
<p><strong>Rating:</strong> {model.Rating}/5</p>
<p><strong>Content:</strong><br/>{HtmlEncode(model.Content).Replace("\n", "<br/>")}</p>
<p><em>Submitted on {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC</em></p>";
        await emailSender.SendAsync(emailSettings.DefaultTo, subject, body);

        TempData["Success"] = "Thanks for your feedback!";
        return RedirectToAction(nameof(Testimonials));
    }

    private static string HtmlEncode(string input) =>
        System.Net.WebUtility.HtmlEncode(input);
}
