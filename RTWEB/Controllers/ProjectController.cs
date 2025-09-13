using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RTWEB.Models;
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

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Project project)
        {
            _unitofWork.ProjectRepository.Save(project);

            TempData["Message"] = "✅ Save Successful";
            TempData["MessageType"] = "success";

            return RedirectToAction("Save");
        }


        public IActionResult Delete(int id)
        {
            _unitofWork.ProjectRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

    }
}
