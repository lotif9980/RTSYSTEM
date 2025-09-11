using Microsoft.AspNetCore.Mvc;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    public class DomainController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public DomainController(IUnitofWork unitofWork)
        {
            this._unitofWork = unitofWork;
        }


        public IActionResult Index()
        {
            var data =_unitofWork.DomainRepository.GetAll();
            return View(data);
        }
    }
}
