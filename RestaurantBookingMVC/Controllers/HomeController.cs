using Microsoft.AspNetCore.Mvc;

namespace RestaurantBookingMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
