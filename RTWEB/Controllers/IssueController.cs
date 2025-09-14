using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;
using System.Threading.Tasks;

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
            if (vm.Issue.Title == null)
            {
                TempData["Message"] = "✅ Data Invalid";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Save");
            }

            var data = new Issue
            {
                Title=vm.Issue.Title,
                ProjectId=vm.Issue.ProjectId
            };

                _unitofWork.IssueRepository.Save(data);

                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";

            return RedirectToAction("Save");
         
        }

        public async Task<IActionResult> Delete(int id)
        {
            var IsUsed=await _unitofWork.IssueRepository.IsUsedAsync(id);
            if (IsUsed)
            {

                TempData["Message"] = "✅ Issue Used in Updates!";
                TempData["MessageType"] = "danger";

                return RedirectToAction("Index");
            }
            _unitofWork.IssueRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }
    }
}
