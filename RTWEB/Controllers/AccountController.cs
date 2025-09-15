using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == DefaultUsername && password == DefaultPassword)
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", username);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "❌ Invalid username or password!";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
