using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RealEstate.Attributes
{
    public class AuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;

            if (context.HttpContext.Session.GetString("LoginUser") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Login", null);
                //context.Result = new RedirectToActionResult("Index", "EsateOffers", null);
            }

            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }   
}
