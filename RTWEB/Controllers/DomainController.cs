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
    public class DomainController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public DomainController(IUnitofWork unitofWork)
        {
            this._unitofWork = unitofWork;
        }


        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var data =_unitofWork.DomainRepository.GetAll()
                .OrderByDescending(d=>d.Id)
                .AsQueryable()
                .ToPagedList(page,pageSize);

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

        public async Task<IActionResult> Delete(int id)
        {
            var isUsed=await _unitofWork.DomainRepository.IsDomainUseAsync(id);
            if (isUsed)
            {
                TempData["Message"] = "✅ Domain Used in Updates!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }
            _unitofWork.DomainRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

    }
}
