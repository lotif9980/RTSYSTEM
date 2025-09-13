using Microsoft.AspNetCore.Mvc;
using RTWEB.Data;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    public class UpdateController : Controller
    {
        protected readonly IUnitofWork _unitofwork;
        public UpdateController(IUnitofWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }


        public IActionResult Index()
        {
            var data = _unitofwork.UpdateRepository.GetUpdates();
            return View(data);
        }
    }
}
