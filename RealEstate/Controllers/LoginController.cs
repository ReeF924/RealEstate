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
            List<LoginUser> found = new();
            var users = this._context.Users!;

            List<User> foundUsers = users.Where(user => user.Username == login.Username).ToList();
            foundUsers = foundUsers.Count == 0 ? users.Where(user => user.Email == login.Username).ToList() : foundUsers;
            foundUsers.ForEach(user => found.Add(user));

            if (found.Count == 0)
            {
                var admins = this._context.Admins!;
                List<Admin> foundAdmins = new();
                foundAdmins = this._context.Admins!.Where(admin => admin.Username == login.Username).ToList();
                foundAdmins = foundAdmins.Count == 0 ? admins.Where(admin => admin.Email == login.Username).ToList() : foundAdmins;
                foundAdmins.ForEach(admin => found.Add(admin));
            }

            if (found.Count == 0)
            {
                this.ModelState.AddModelError("Username", "Username or password incorrect");
                return RedirectToAction("Login", "Login", login);
            }

            LoginUser loginUser = found.First();
            if (!BCrypt.Net.BCrypt.Verify(login.Password, loginUser.Password))
            {
                this.ModelState.AddModelError("Username", "Username or password incorrect");
                return RedirectToAction("Login", "Login", login);
            }

            this.HttpContext.Session.SetString("LoginUser", JsonSerializer.Serialize(loginUser));

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
            LoginUser user = input;
            this.HttpContext.Session.SetString("LoginUser", JsonSerializer.Serialize(user));

            return RedirectToAction("Index", "EstateOffers");
        }
        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("LoginUser");

            return RedirectToAction("Index", "EstateOffers");
        }

        //Json web token JWT
    }
}
