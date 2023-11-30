using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class EstateOffersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
