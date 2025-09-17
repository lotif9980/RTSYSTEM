using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RTWEB.Repository;
using RTWEB.ViewModel;

namespace RTWEB.Controllers
{
    public class ReportController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public ReportController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult IssuePendingReport()
        {
            var project = _unitofWork.ProjectRepository.GetProjects()
                 .Select(w=> new SelectListItem
                 {
                     Value=w.Id.ToString(),
                     Text=w.ProjectName
                 }).ToList();

            ViewBag.Project=project;

            return View(new List<IssueVM>());
        }

        [HttpPost]
        public IActionResult IssuePendingDataShow(int ? projectId)
        {
            var project = _unitofWork.ProjectRepository.GetProjects()
               .Select(w => new SelectListItem
               {
                   Value = w.Id.ToString(),
                   Text = w.ProjectName
               }).ToList();

            ViewBag.Project = project;

            var data = _unitofWork.ReportRepository.PendingIssueReport(projectId);
            return View("IssuePendingReport", data);
        }
    }
}
