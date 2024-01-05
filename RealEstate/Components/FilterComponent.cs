using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Models.ComponentModels;
using System.Text;
using System.Text.Json;

namespace RealEstate.Components
{
    public class FilterComponent : ViewComponent
    {
        public IViewComponentResult Invoke(FilterComponentParameter param)
        {
            FilterModel filter = param.Filter;

            filter.PriceMin ??= 0;
            filter.PriceMax ??= param.MaxPrice;

            this.ViewBag.MaxPrice = param.MaxPrice;
            this.ViewBag.Filter = filter;
            this.ViewBag.Regions = param.Regions;
            return View();
        }
    }
}
