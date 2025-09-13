using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;

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


        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Domain domain)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.DomainRepository.Save(domain);

                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";

                return RedirectToAction("Index");
            }
            return View(domain);
        }

        public IActionResult Delete(int id)
        {
            _unitofWork.DomainRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

    }
}
