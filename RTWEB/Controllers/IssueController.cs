using Microsoft.AspNetCore.Mvc;
using RTWEB.Repository;

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
    }
}
