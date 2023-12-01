using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;

namespace RealEstate.Controllers
{
    public abstract class BaseEstateController : Controller
    {
        protected Context _context = new();
    }
}
