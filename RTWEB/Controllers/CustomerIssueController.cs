using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    public class CustomerIssueController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public CustomerIssueController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }


        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data = _unitofWork.CustomerIssueRepository.GetAll()
                    .OrderByDescending(d => d.Id)
                    .AsQueryable()
                    .ToPagedList(page, pageSize);

            return View(data);
        }
    }
}
