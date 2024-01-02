using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;

namespace RealEstate.Components
{
    public class FilterComponent : ViewComponent
    {
        public IViewComponentResult Invoke(KeyValuePair<ulong, List<string>> param)
        {
            FilterModel filter = new();
            filter.PriceMin = 0;
            filter.PriceMax = param.Key;

            this.ViewBag.Filter = filter;
            this.ViewBag.Regions = param.Value;
            return View();
        }
    }
}
