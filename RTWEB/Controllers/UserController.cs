using Microsoft.AspNetCore.Mvc;
using RTWEB.Repository;
using RTWEB.ViewModel;

namespace RTWEB.Controllers
{
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


    }
}
