using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;

namespace RTWEB.Controllers
{
    public class IssueController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
       
        public IssueController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        public IActionResult Index()
        {
            var data = _unitofWork.IssueRepository.GetIssues();
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var vm = new IssueSaveVm
            {
                Issue = new Models.Issue(),
                Projects=_unitofWork.ProjectRepository.GetProjects()
            };

            return View(vm);
        }


        [HttpPost]
        public IActionResult Save(IssueSaveVm vm)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.IssueRepository.Save(vm.Issue);

                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";

                return RedirectToAction("Save");
            }

            var returnVm = new IssueSaveVm
            {
                Issue = new Models.Issue(),
                Projects = _unitofWork.ProjectRepository.GetProjects()
            };
            return View(returnVm);
        }

        public IActionResult Delete(int id)
        {
            _unitofWork.IssueRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }
    }
}
