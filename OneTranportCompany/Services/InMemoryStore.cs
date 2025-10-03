using OneTransportCompany.Models;

namespace OneTransportCompany.Services;

public class InMemoryStore : IInMemoryStore
{
    public List<ContactFormModel> Contacts { get; } = new();
    public List<SendRequestModel> Requests { get; } = new();
    public List<Testimonial> Testimonials { get; } = new()
    {
        new Testimonial { ClientName = "Acme Ltd.", Rating = 5, Content = "On-time delivery and professional drivers." },
        new Testimonial { ClientName = "Global Foods", Rating = 4, Content = "Great communication and fair pricing." },
        new Testimonial { ClientName = "Tech Cargo", Rating = 5, Content = "Handled fragile goods perfectly. Highly recommended." },
    };

    public void AddContact(ContactFormModel model) => Contacts.Add(model);
    public void AddRequest(SendRequestModel model) => Requests.Add(model);
    public void AddTestimonial(Testimonial model) => Testimonials.Add(model);
}
