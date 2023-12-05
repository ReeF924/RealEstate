using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using BCrypt.Net;
using RealEstate.Models.LoginModels;

namespace RealEstate.Controllers
{
    public class LoginController : BaseEstateController
    {
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new User());
        }


        [HttpPost]
        public IActionResult SignUp(User user)
        {
            this._context.Users!.Add(user);
            this._context.SaveChanges();
            this.HttpContext.Session.SetString("user", user.Username);

            return RedirectToAction("Index", "EstateOffers");
        }


        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            List<User> users = this._context.Users!.ToList();

            List<User> found = users.Where(user => user.Username == login.Username).ToList();

            found = found.Count == 0 ? users.Where(user => user.Email == login.Username).ToList() : found;

            if (found.Count != 1)
            {
                this.ModelState.AddModelError("Username", "Username or password incorrect");
                return RedirectToAction("Index", "EstateOffers", new { succesfulLogin = false });
            }

            User user = found.First();

            if (login.Password != user.Password)
            //if(!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                this.ModelState.AddModelError("Username", "Username or password incorrect");
                return RedirectToAction("Index", "EstateOffers", new { succesfulLogin = false });
            }

            this.HttpContext.Session.SetString("user", user.Username);

            return RedirectToAction("Index", "EstateOffers", new { succesfulLogin = true });
        }


    }
}
