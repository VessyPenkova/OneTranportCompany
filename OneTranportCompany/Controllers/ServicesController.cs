using Microsoft.AspNetCore.Mvc;

namespace OneTransportCompany.Controllers;

public class ServicesController : Controller
{
    public IActionResult Index() => View();                 // /Services
    public IActionResult AirCharter() => View();            // /Services/AirCharter
    public IActionResult OnBoardCourier() => View();        // /Services/OnBoardCourier
    public IActionResult AirFreight() => View();            // /Services/AirFreight
    public IActionResult RoadFreight() => View();           // /Services/RoadFreight
    public IActionResult CustomsClearance() => View();      // /Services/CustomsClearance
    public IActionResult Tracking() => View();              // /Services/Tracking
    public IActionResult Warehousing() => View();           // /Services/Warehousing
    public IActionResult Additional() => View();            // /Services/Additional
}
