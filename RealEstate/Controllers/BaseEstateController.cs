using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Models.DatabaseModels;

namespace RealEstate.Controllers
{
    public abstract class BaseEstateController : Controller
    {
        protected Context _context = new();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            this.ViewBag.user = this.HttpContext.Session.GetString("IdUser");
            
            //give all action data to View
            Dictionary<string, string> routeData = this.HttpContext.Request.Query.ToDictionary(item => item.Key, item => item.Value.ToString()!);
            this.HttpContext.Request.RouteValues.ForEachExt(item => routeData[item.Key] = item.Value!.ToString()!);


            this.ViewBag.RouteData = routeData;
        }
    }
}
