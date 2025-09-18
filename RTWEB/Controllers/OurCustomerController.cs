using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    [Authorize]
    public class OurCustomerController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public OurCustomerController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }

        public IActionResult Index(int page=1, int pageSize= 10)
        {
            var data =_unitofWork.OurCustomerRepository.GetAll()
                    .OrderByDescending(d => d.Id)
                    .AsQueryable()
                    .ToPagedList(page, pageSize); ;
            return View(data);
        }
    }
}
