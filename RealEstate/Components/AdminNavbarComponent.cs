using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
namespace RealEstate.Components
{
    public class AdminNavbarComponent : ViewComponent
    {
        public IViewComponentResult Invoke(User user)
        {
            this.ViewBag.User = user; 

            return View();
        }

    }
}
