using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RTWEB.Controllers
{
    public class AccountController : Controller
    {
        private const string DefaultUsername = "admin";
        private const string DefaultPassword = "1234";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
       
        public IActionResult Login()
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    if (username == DefaultUsername && password == DefaultPassword)
        //    {
        //        HttpContext.Session.SetString("IsLoggedIn", "true");
        //        HttpContext.Session.SetString("Username", username);

        //        return RedirectToAction("Index", "Home");
        //    }

        //    ViewBag.Error = "❌ Invalid username or password!";
        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            // কেবল demo fixed credentials
            if (username == DefaultUsername && password == DefaultPassword)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");

                await HttpContext.SignInAsync("MyCookieAuth",
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            // invalid credentials
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Account");
        }
    }
}
