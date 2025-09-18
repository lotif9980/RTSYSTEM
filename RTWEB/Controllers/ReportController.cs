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


        #region issue Pending Report
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
        public IActionResult IssuePendingReport(int ? projectId)
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
        #endregion


        [HttpGet]
        public IActionResult DominWiseUpdate()
        {
            var domain = _unitofWork.DomainRepository.GetAll()
                   .Select(w => new SelectListItem
                   {
                       Value = w.Id.ToString(),
                       Text = w.DomainName
                   }).ToList();

            ViewBag.Domain = domain;

            return View(new List<DomainReportVM>());
        }

        [HttpPost]
        public IActionResult DominWiseUpdate(int?domainId=null)
        {
            var domain = _unitofWork.DomainRepository.GetAll()
                   .Select(w => new SelectListItem
                   {
                       Value = w.Id.ToString(),
                       Text = w.DomainName
                   }).ToList();

            ViewBag.Domain = domain;

            var data=_unitofWork.ReportRepository.DomainWiseUpdate(domainId);
            return View("DominWiseUpdate", data);
        }

        [HttpGet]
        public IActionResult DomainUpdateList()
        {
            var domain = _unitofWork.DomainRepository.GetAll()
                   .Select(w => new SelectListItem
                   {
                       Value = w.Id.ToString(),
                       Text = w.DomainName
                   }).ToList();

            ViewBag.Domain = domain;

            return View();
        }

        [HttpPost]
        public IActionResult DomainUpdateList(int? domainId = null)
        {
            var domain = _unitofWork.DomainRepository.GetAll()
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.DomainName
                }).ToList();

            ViewBag.Domain = domain;

            var data =_unitofWork.ReportRepository.DomainUpdateList(domainId);
            return View("DomainUpdateList", data);
        }




    }
}
