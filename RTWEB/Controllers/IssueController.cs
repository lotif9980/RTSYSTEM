using HMSYSTEM.Helpers;
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


        public IActionResult Index(int page=1, int pageSize = 10)
        {
            var data = _unitofWork.IssueRepository.GetIssues()
                .OrderByDescending(d=>d.Id)
                .AsQueryable()
                .ToPagedList(page,pageSize);
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
                Title = vm.Issue.Title,
                ProjectId = vm.Issue.ProjectId,
                Status = vm.Issue.Status = Enum.IssueStatus.pending

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
