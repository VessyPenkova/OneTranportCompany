using Microsoft.AspNetCore.Mvc;

namespace OneTransportCompany.Controllers;

public class AboutController : Controller
{
    // /About
    public IActionResult Index() => View();

    // /About/Industries
    public IActionResult Industries() => View();

    // /About/People
    public IActionResult People() => View();

    // /About/WhyChooseUs
    public IActionResult WhyChooseUs() => View();

    // /About/Awards
    public IActionResult Awards() => View();

    // /About/Policies
    public IActionResult Policies() => View();
}
