using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;

namespace RTWEB.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public UserController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        public IActionResult Index()
        {
           var data=_unitofWork.UserRepository.GetAll();
           return View(data);
        }

        public IActionResult Save()
        {
            var data = new UserSaveVM
            {
                User=new Models.User(),
                Role=_unitofWork.RoleRepository.GetAll(),
            };
            return View(data);
        }


        [HttpPost]
        public IActionResult Save(UserSaveVM model)
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
                };

                return View(vm);
            }

        
            var save = new User
            {
                Name = model.User.Name,
                PhoneNo = model.User.PhoneNo,
                Email = model.User.Email,
                RoleId = model.User.RoleId,
                UserName = model.User.UserName,
                Password = model.User.Password,
                Status = true
            };

            _unitofWork.UserRepository.Save(save);
            var result=_unitofWork.Complete();

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
         

            return RedirectToAction("Save");
        }



       

    }
}
