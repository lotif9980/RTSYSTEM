using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;
using System.Security.Claims;

namespace RTWEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public AccountController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        //private const string DefaultUsername = "admin";
        //private const string DefaultPassword = "1234";
        //private const string UName = "support";
        //private const string UPassword = "Test12";


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

        #region Old Version

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

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(string username, string password)
        //{
        //  if((username == DefaultUsername && password == DefaultPassword)|| (username == UName && password == UPassword))
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, username)
        //        };

        //        var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");

        //        var authProperties = new AuthenticationProperties
        //        {
        //            IsPersistent = false, // 🔹 browser বন্ধ করলে cookie থাকবে না
        //            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // optional timeout
        //        };

        //        await HttpContext.SignInAsync(
        //            "MyCookieAuth",
        //            new ClaimsPrincipal(claimsIdentity),
        //            authProperties);

        //        return RedirectToAction("Index", "Home");
        //  }

        //    ModelState.AddModelError("", "Invalid username or password");
        //    return View();
        //}

        #endregion End old version

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
           
            var user = _unitofWork.UserRepository
                        .GetFirstOrDefault(username, password);

            if (user != null)
            {
                if (user.Status ==false)
                {
                    ModelState.AddModelError("", "⚠️ Your account is inactive. Please contact the admin.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim("EmployeeId", user.EmployeeId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(
                    "MyCookieAuth",
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "❌ Invalid username or password!");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Account");
        }


        #region login before user create

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SaveUser()
        {
            var data = new UserSaveVM
            {
                User = new Models.User(),
                Role = _unitofWork.RoleRepository.GetAll(),
                Team = _unitofWork.TeamRepository.GetTeams()
            };
            return View(data);
        }


        [HttpPost]
        public IActionResult SaveUser(UserSaveVM model)
        {

            if (model.User.RoleId == null ||
               string.IsNullOrEmpty(model.User.UserName) ||
               string.IsNullOrEmpty(model.User.Password))
            {
                TempData["Message"] = "❌ Please input valid data";
                TempData["MessageType"] = "danger";

                var vm = new UserSaveVM
                {
                    User = new Models.User(),
                    Role = _unitofWork.RoleRepository.GetAll(),
                    Team = _unitofWork.TeamRepository.GetTeams()
                };

                return View(vm);
            }


            bool existingName = _unitofWork.UserRepository.ExestingCheck(model.User.UserName);
            if (existingName)
            {
                TempData["Message"] = "❌ Username already exists";
                TempData["MessageType"] = "danger";

                var vm = new UserSaveVM
                {
                    User = new Models.User(),
                    Role = _unitofWork.RoleRepository.GetAll(),
                    Team = _unitofWork.TeamRepository.GetTeams()
                };

                return View(vm);
            }

            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null, model.User.Password);

            var save = new User
            {
                Name = model.User.Name,
                PhoneNo = model.User.PhoneNo,
                Email = model.User.Email,
                RoleId = model.User.RoleId,
                UserName = model.User.UserName,
                Password = hashedPassword,
                Status = true,
                EmployeeId = model.User.EmployeeId,
            };

            _unitofWork.UserRepository.Save(save);
            var result = _unitofWork.Complete();

            if (result > 0)
            {
                TempData["Message"] = "✅ Saved successfully";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Saved Faild";
                TempData["MessageType"] = "success";
            }



            return RedirectToAction("Login");
        }
        #endregion
    }
}
