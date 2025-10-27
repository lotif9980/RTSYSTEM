using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;
using System.Threading.Tasks;

namespace RTWEB.Controllers
{
    [Authorize]
    public class CustomerIssueController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public CustomerIssueController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }


        public IActionResult Index()
        {
            var data = _unitofWork.CustomerIssueRepository.GetAll()
                    .OrderByDescending(d => d.Id)
                    .AsQueryable();
            return View(data);
        }

        public IActionResult SolvedIssue()
        {
            var data = _unitofWork.CustomerIssueRepository.GetSolved()
                    .OrderByDescending(d => d.Id)
                    .AsQueryable();
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var data = new CustomerIssueSaveVM
            {

                CustomerIssue = new List<CustomerIssue> { new CustomerIssue() },
                Domain=_unitofWork.DomainRepository.GetAll(),
                OurCustomer=_unitofWork.OurCustomerRepository.GetAll(),
            };
            return View(data);
        }

        [HttpGet]
        public IActionResult GetDomainByCustomer(int domainId)
        {
            var customer = _unitofWork.OurCustomerRepository.CustomerList()
                          .Where(d => d.DomainId == domainId)
                          .Select(p => new
                          {
                              p.Id,
                              p.CustomerName

                          });
            return Json(customer);
        }


        [HttpPost]
        public IActionResult Save(CustomerIssueSaveVM vmodel)
        {
            if (vmodel.CustomerIssue==null || !vmodel.CustomerIssue.Any())
            {
                TempData["Message"] = "❌ Please select Domain and add at least one Problem";
                TempData["MessageType"] = "danger";

                var data = new CustomerIssueSaveVM
                {

                    CustomerIssue = new List<CustomerIssue> { new CustomerIssue() },
                    Domain = _unitofWork.DomainRepository.GetAll(),
                    OurCustomer = _unitofWork.OurCustomerRepository.GetAll(),
                };
                return View(data);
            }

            foreach(var item in vmodel.CustomerIssue)
            {
                if (!string.IsNullOrWhiteSpace(item.Problem))
                {
                    var data = new CustomerIssue
                    {
                        DomainId=item.DomainId,
                        CustomerId=item.CustomerId,
                        Problem=item.Problem,
                        Status=Enum.CustomerIssueStatus.pending
                    };
                    _unitofWork.CustomerIssueRepository.Save(data);
                }
            }
            _unitofWork.Complete();
            TempData["Message"] = "✅ Issues Saved Successfully";
            TempData["MessageType"] = "success";
            return RedirectToAction("Save");

        }
        [HttpGet]
        public IActionResult SearchCustomerName(string name)
        {
            name = name?.Trim().ToLower() ?? "";

            var data=_unitofWork.CustomerIssueRepository.GetAll()
                     .OrderByDescending(p=>p.Id)
                     .Where(m=> !string.IsNullOrEmpty(m.CustomerName) && m.CustomerName.ToLower().Contains(name))
                     .Select(m=> new
                     {
                         id = m.Id,
                         customer =m.CustomerName,
                         domain=m.Domainname,
                         problem=m.Problem,
                         status=Enum.CustomerIssueStatus.pending.ToString()
                     });

            return Json(data);
        }

        [HttpGet]
        public IActionResult SearchCustomerNameSolvedList(string name)
        {
            name = name?.Trim().ToLower() ?? "";

            var data = _unitofWork.CustomerIssueRepository.GetSolved()
                     .OrderByDescending(p => p.Id)
                     .Where(m => !string.IsNullOrEmpty(m.CustomerName) && m.CustomerName.ToLower().Contains(name))
                     .Select(m => new
                     {
                         id=m.Id,
                         customer = m.CustomerName,
                         domain = m.Domainname,
                         problem = m.Problem,
                         status = Enum.CustomerIssueStatus.solved.ToString()
                     });

            return Json(data);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var isUsed=await _unitofWork.CustomerIssueRepository.IsUsedIssue(id);

            if (isUsed)
            {
                TempData["Message"] = "❌ Issue Already Solved";
                TempData["MessageType"] = "danger";

                return RedirectToAction("Index");
            }

           _unitofWork.CustomerIssueRepository.Delete(id);

           var result=_unitofWork.Complete();

            if(result > 0)
            {
                TempData["Message"] = "✅ Delete Successfully";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Delete Unsuccessful";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }

    }
}
