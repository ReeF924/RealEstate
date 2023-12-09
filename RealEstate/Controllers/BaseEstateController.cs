using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Models.DatabaseModels;
using System.Text.Json;

namespace RealEstate.Controllers
{
    public abstract class BaseEstateController : Controller
    {
        protected Context _context = new();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var value = this.HttpContext.Session.GetString("LoginUser");

            if (value != null)
                this.ViewBag.LoginUser = JsonSerializer.Deserialize<LoginUser>(value);

            //give all action data to View
            Dictionary<string, string> routeData = this.HttpContext.Request.Query.ToDictionary(item => item.Key, item => item.Value.ToString()!);
            this.HttpContext.Request.RouteValues.ForEachExt(item => routeData[item.Key] = item.Value!.ToString()!);

            this.ViewBag.RouteData = routeData;
        }
    }
}
