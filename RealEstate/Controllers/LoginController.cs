using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using BCrypt.Net;
using RealEstate.Models.LoginModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace RealEstate.Controllers
{
    public class LoginController : BaseEstateController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //this.ViewBag.NavUnderline = "Login";
            base.OnActionExecuting(context);
            this.ViewBag.FooterVisible = false;
            this.ViewBag.NavUnderline = "Login";
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Index(LoginModel login)
        {
            var users = this._context.Users!;

            List<User> found = users.Where(user => user.Username == login.Username).ToList();
            found = found.Count == 0 ? users.Where(user => user.Email == login.Username).ToList() : found;

            if (found.Count == 0)
            {
                this.ModelState.AddModelError("Username", "Username or password incorrect");
                return RedirectToAction("Login", "Login", login);
            }

            User user = found.First();
            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                this.ModelState.AddModelError("Username", "Username or password incorrect");
                return RedirectToAction("Login", "Login", login);
            }

            this.HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));

            //return RedirectToAction(action, controller, new { succesfulLogin = true });
            return RedirectToAction("Index", "EstateOffers");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new User());
        }


        [HttpPost]
        public IActionResult SignUp(User input)
        {
            input.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);

            this._context.Users!.Add(input);
            this._context.SaveChanges();
            User user = input;
            this.HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));

            return RedirectToAction("Index", "EstateOffers");
        }
        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("User");

            return RedirectToAction("Index", "EstateOffers");
        }

        //Json web token JWT
    }
}
