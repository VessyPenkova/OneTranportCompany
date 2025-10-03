using OneTransportCompany.Models;

namespace OneTransportCompany.Services;

public interface IInMemoryStore
{
    List<ContactFormModel> Contacts { get; }
    List<SendRequestModel> Requests { get; }
    List<Testimonial> Testimonials { get; }

    void AddContact(ContactFormModel model);
    void AddRequest(SendRequestModel model);
    void AddTestimonial(Testimonial model);
}
