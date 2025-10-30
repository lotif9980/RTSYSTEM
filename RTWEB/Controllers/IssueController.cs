using RTWEB.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;
using System.Threading.Tasks;

namespace RTWEB.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
       
        public IssueController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [Authorize]
        public IActionResult Index()
        {
            var data = _unitofWork.IssueRepository.GetIssues()
                .OrderByDescending(d => d.Id)
                .AsQueryable();
            return View(data);
        }

        [Authorize]
        public IActionResult Solved()
        {
            var data = _unitofWork.IssueRepository.GetSolved()
                .OrderByDescending(d => d.Id)
                .AsQueryable();
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var vm = new IssueSaveVm
            {
                Projects = _unitofWork.ProjectRepository.GetProjects(),
                Issues = new List<Issue> { new Issue() } 
            };

            return View(vm);
        }

        #region Old Issue Save Verison
        //[HttpPost]
        //public IActionResult Save(IssueSaveVm vm)
        //{
        //    if (vm.Issue.Title == null)
        //    {
        //        TempData["Message"] = "✅ Data Invalid";
        //        TempData["MessageType"] = "danger";
        //        return RedirectToAction("Save");
        //    }

        //    var data = new Issue
        //    {
        //        Title = vm.Issue.Title,
        //        ProjectId = vm.Issue.ProjectId,
        //        Status = vm.Issue.Status = Enum.IssueStatus.pending

        //    };

        //        _unitofWork.IssueRepository.Save(data);

        //        TempData["Message"] = "✅ Save Successful";
        //        TempData["MessageType"] = "success";

        //    return RedirectToAction("Save");

        //}
        #endregion


        [HttpPost]
        public IActionResult Save(IssueSaveVm vm)
        {
            //var exestingIssue=_unitofWork.IssueRepository.ExestingIssue(vm.ProjectId,vm.I)

            if (vm.ProjectId == 0 || vm.Issues == null || !vm.Issues.Any())
            {
                TempData["Message"] = "❌ Please select Project and add at least one Title";
                TempData["MessageType"] = "danger";
                vm.Projects = _unitofWork.ProjectRepository.GetProjects();
                return View(vm);
            }



            foreach (var issue in vm.Issues)
            {
                if (!string.IsNullOrWhiteSpace(issue.Title))
                {
                    var exestingIssue=_unitofWork.IssueRepository.ExestingIssue(vm.ProjectId, issue.Title);
                    if (exestingIssue)
                    {
                        TempData["Message"] = "❌ Already Added";
                        TempData["MessageType"] = "danger";

                        vm.Projects = _unitofWork.ProjectRepository.GetProjects();

                        return View(vm);
                    }


                    var data = new Issue
                    {
                        ProjectId = vm.ProjectId,
                        Title = issue.Title,
                        Status = Enum.IssueStatus.Pending
                    };
                    _unitofWork.IssueRepository.Save(data);
                }
            }
            _unitofWork.Complete();
            TempData["Message"] = "✅ Issues Saved Successfully";
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
            _unitofWork.Complete();

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

     
    }
}
