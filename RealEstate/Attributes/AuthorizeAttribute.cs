using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Models.DatabaseModels;
using System.Text.Json;

namespace RealEstate.Attributes
{
    public class AuthorizeAttribute : Attribute, IActionFilter
    {
        private bool UserAllowed = true;

        public AuthorizeAttribute(bool userAllowed)
        {
                this.UserAllowed = userAllowed;
        }

        public AuthorizeAttribute()
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;

            if (context.HttpContext.Session.GetString("User") == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            if(UserAllowed)
                return;

            string data= context.HttpContext.Session.GetString("User")!;
            User user = JsonSerializer.Deserialize<User>(data)!;

            if (user.Type == 'u')
                context.Result = new RedirectToActionResult("Index", "Home", null);
            

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }   
}
