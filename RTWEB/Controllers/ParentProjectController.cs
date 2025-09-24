using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    [Authorize]
    public class ParentProjectController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public ParentProjectController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }
        public IActionResult Index()
        {
            var data =_unitofWork.ParentProjectsRepository.GetAll();
            return View(data);
        }
    }
}
