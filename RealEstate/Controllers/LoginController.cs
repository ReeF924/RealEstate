using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using BCrypt.Net;
using RealEstate.Models.LoginModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using RealEstate.Services;
using RealEstate.Models;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Mozilla;

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
        public IActionResult Index(bool forgotPassword = false)
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
                return RedirectToAction("Index", "Login", login);
            }

            User user = found.First();
            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                this.ModelState.AddModelError("Username", "Username or password incorrect");
                return RedirectToAction("Index", "Login", login);
            }

            this.HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));

            //return RedirectToAction(action, controller, new { succesfulLogin = true });
            return RedirectToAction("Index", "EstateOffers");
        }

        [HttpGet]
        public IActionResult SignUp(int? idUser = null)
        {
            User user;
            if(idUser != null)
            {
                user = this._context.Users!.Where(user => user.Id == idUser).First();
                user.Password = "";
            }
            else
                user = new User();

            this.ViewBag.IdUser = idUser;   
            this.ViewBag.Type = idUser == null ? "Create" : "Edit";
            return View(user);
        }

        [HttpPost]
        public IActionResult CreateUser(User input)
        {
            //validace
            input.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);

            this._context.Users!.Add(input);
            this._context.SaveChanges();
            User user = input;
            this.HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));

            return RedirectToAction("Index", "EstateOffers");
        }

        [HttpPost]
        public IActionResult EditUser(User input, int idUser)
        {
            //validace
            User user = this._context.Users!.Where(user => user.Id == idUser).First();
            user.Username = input.Username;
            user.Email = input.Email;
            user.Password = !String.IsNullOrEmpty(input.Password) ? BCrypt.Net.BCrypt.HashPassword(input.Password) : user.Password;
            user.Name = input.Name;
            user.Surname = input.Surname;
            user.PhoneNumber = input.PhoneNumber;


            this._context.SaveChanges();
            this.HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));

            return RedirectToAction("Index", "EstateOffers");
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("User");

            return RedirectToAction("Index", "EstateOffers");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(StringInputModel emailModel)
        {
            var user = this._context.Users.Where(user => user.Email == emailModel.Value).FirstOrDefault();

            if (user != null)
            {
                string passwd = PasswordService.GeneratePassword();
                user.Password = BCrypt.Net.BCrypt.HashPassword(passwd);
                this._context.SaveChanges();
                MailService.SendMail(user, passwd);
            }

            return RedirectToAction("Index", "EstateOffers", new { forgotPassword = true });
        }

        //Json web token JWT
    }
}
