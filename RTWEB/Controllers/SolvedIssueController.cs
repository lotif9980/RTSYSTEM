using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;

namespace RTWEB.Controllers
{
    public class SolvedIssueController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public SolvedIssueController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        public IActionResult Index(int page=1,int pageSize=10)
        {
            var data =_unitofWork.SolvedIssueRepository.GetSolvedIssue()
                        .OrderByDescending(d => d.Id)
                        .AsQueryable()
                        .ToPagedList(page, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var data = new CSPViewModel
            {
                SolvedIssue = new Models.SolvedIssue(),
                Teams = _unitofWork.TeamRepository.GetTeams(),
                Domains = _unitofWork.DomainRepository.GetAll(),
                OurCustomers = _unitofWork.OurCustomerRepository.GetAll(),
                CustomerIssue = _unitofWork.CustomerIssueRepository.GetAll()
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult Save(CSPViewModel model)
        {
            var solvedIssue = new SolvedIssue
            {
                CustomerId=model.SolvedIssue.CustomerId,
                SolvedBy=model.SolvedIssue.SolvedBy,
                SolvedDate=model.SolvedIssue.SolvedDate,
                DomainId=model.SolvedIssue.DomainId,
                Status=model.SolvedIssue.Status
                
            };

            if (model.SolveDetails != null && model.SolveDetails.Count > 0)
            {
                solvedIssue.SolvedDetails = model.SolveDetails.Select(d => new SolvedDetail
                {
                    IssueId = d.IssueId,
                    SolutionDetails = d.SolutionDetails,
                    
                }).ToList();
            }

            _unitofWork.SolvedIssueRepository.Save(solvedIssue);


            if (model.SolveDetails!=null && model.SolveDetails.Count > 0)
            {
                foreach(var item in model.SolveDetails)
                {
                    _unitofWork.CustomerIssueRepository.UpdateStatus((int)item.IssueId, Enum.CustomerIssueStatus.solved);
                }
            }

            var result = _unitofWork.Complete();

            if (result > 0)
            {
                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Save Failed";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }



        public IActionResult DomainWiseCustomer(int domainId)
        {
            var data = _unitofWork.OurCustomerRepository.CustomerList()
                .Where(p => p.DomainId == domainId)
                .Select(b => new
                {
                    b.Id,
                    b.CustomerName
                });

            return Json(data);
        }

        public IActionResult CustomerWiseProlem(int customerId)
        {
            var data = _unitofWork.CustomerIssueRepository.GetAll()
                .Where(p => p.CustomerId == customerId)
                .Select(b => new
                {
                    b.Id,
                    b.Problem
                });

            return Json(data);
        }


        public IActionResult Delete(int id)
        {
            var solvedDetails=_unitofWork.SolvedIssueRepository.GetById(id);
            if(solvedDetails != null && solvedDetails.Any())
            {
                foreach(var item in solvedDetails)
                {
                    _unitofWork.CustomerIssueRepository.UpdateStatus((int)item.IssueId,Enum.CustomerIssueStatus.pending);
                }
            }

            _unitofWork.SolvedIssueRepository.Delete(id);
            _unitofWork.Complete();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var data =_unitofWork.SolvedIssueRepository.Details(id);
            return View(data);
        }
    }
}
