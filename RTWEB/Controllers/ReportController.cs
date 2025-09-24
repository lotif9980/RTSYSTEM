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

        [HttpGet]
        public IActionResult ProjectWiseDomain()
        {
            var project = _unitofWork.ProjectRepository.GetProjects()
                   .Select(w => new SelectListItem
                   {
                       Value = w.Id.ToString(),
                       Text = w.ProjectName
                   }).ToList();

            ViewBag.Project = project;

            return View();
        }

        [HttpPost]
        public IActionResult ProjectWiseDomain(int? projectId = null)
        {
            var domain = _unitofWork.ProjectRepository.GetProjects()
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.ProjectName
                }).ToList();

            ViewBag.Domain = domain;

            var data = _unitofWork.ReportRepository.ProjectWiseDomain(projectId);
            return View("ProjectWiseDomain", data);
        }


        [HttpGet]
        public IActionResult SolvedIssueReport()
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
        public IActionResult SolvedIssueReport(int? domainId = null, int? customerId = null)
        {
            var domain = _unitofWork.DomainRepository.GetAll()
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.DomainName
                }).ToList();
            ViewBag.Domain = domain;

            var data = _unitofWork.ReportRepository.CustomerSolvedIssue(domainId, customerId);
            return View("SolvedIssueReport", data); 
        }




        [HttpGet]
        public IActionResult DailySupportReport()
        {
            var domain = _unitofWork.DomainRepository.GetAll()
                        .Select(w => new SelectListItem
                        {
                            Value = w.Id.ToString(),
                            Text = w.DomainName
                        }).ToList();
            ViewBag.Domain = domain;

            var team=_unitofWork.TeamRepository.GetTeams()
                        .Select(w=>new SelectListItem
                        {
                            Value= w.Id.ToString(),
                            Text=w.Name
                        }).ToList();
            ViewBag.Team = team;

            return View();
        }


        [HttpPost]
        public IActionResult DailySupportReport(DateTime? fromDate, DateTime? toDate, int? domainId = null, int? customerId = null,int? solvedBy=null )
        {
            if (!fromDate.HasValue || !toDate.HasValue)
            {
                return View(new List<CustomerSolvedIssueVM>());
            }

            var domain = _unitofWork.DomainRepository.GetAll()
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.DomainName
                }).ToList();
            ViewBag.Domain = domain;

            var team = _unitofWork.TeamRepository.GetTeams()
                        .Select(w => new SelectListItem
                        {
                            Value = w.Id.ToString(),
                            Text = w.Name
                        }).ToList();
            ViewBag.Team = team;

            var data = _unitofWork.ReportRepository.CustomerDailySupport(fromDate.Value, toDate.Value, domainId, customerId,solvedBy);
            return View("DailySupportReport", data);
        }

        [HttpGet]
        public IActionResult CustomerLedger()
        {
            var ourCustomer = _unitofWork.OurCustomerRepository.GetAll()
                       .Select(w => new SelectListItem
                       {
                           Value = w.Id.ToString(),
                           Text = w.CustomerName
                       }).ToList();
            ViewBag.OurCustomer = ourCustomer;
         
            return View();
        }

        [HttpPost]
        public IActionResult CustomerLedger(int customerId )
        {
            //if (!fromDate.HasValue || !toDate.HasValue)
            //{
            //    return View(new List<CustomerIssueVM>());
            //}

            var ourCustomer = _unitofWork.OurCustomerRepository.GetAll()
                      .Select(w => new SelectListItem
                      {
                          Value = w.Id.ToString(),
                          Text = w.CustomerName
                      }).ToList();
            ViewBag.OurCustomer = ourCustomer;


            var data =_unitofWork.ReportRepository.CustomerLedger(customerId);
            return View("CustomerLedger", data);
        }


        [HttpGet]
        public IActionResult PendingIssueReport()
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
        public IActionResult PendingIssueReport(int? domainId = null, int? customerId = null)
        {
            var domain = _unitofWork.DomainRepository.GetAll()
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.DomainName
                }).ToList();
            ViewBag.Domain = domain;

            var data = _unitofWork.ReportRepository.PendingSupport( domainId , customerId );
            return View("PendingIssueReport", data);
        }


    }
}
