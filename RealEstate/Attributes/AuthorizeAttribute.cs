using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Models.DatabaseModels;
using System.Text.Json;

namespace RealEstate.Attributes
{
    public class AuthorizeAttribute : Attribute, IActionFilter
    {
        private bool UsersAllowed = true;
        private bool BrokersAllowed = true;

        public AuthorizeAttribute(bool usersAllowed = true, bool brokersAllowed = true)
        {
            this.UsersAllowed = usersAllowed;
            this.BrokersAllowed = brokersAllowed;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;

            if (context.HttpContext.Session.GetString("User") == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }


            string data = context.HttpContext.Session.GetString("User")!;
            User user = JsonSerializer.Deserialize<User>(data)!;

            if (user.Type == 'b' && !this.BrokersAllowed)
                context.Result = new RedirectToActionResult("Index", "Home", null);

            if (user.Type == 'u' && !this.UsersAllowed)
                context.Result = new RedirectToActionResult("Index", "Home", null);


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
