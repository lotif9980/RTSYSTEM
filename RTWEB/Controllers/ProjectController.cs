using Microsoft.AspNetCore.Mvc;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    public class ProjectController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public ProjectController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }

        public IActionResult Index()
        {
            var data = _unitofWork.ProjectRepository.GetProjects();
            return View(data);
        }
    }
}
