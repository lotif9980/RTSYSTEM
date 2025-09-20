using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Enum;
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
            if (_unitofWork.DomainRepository.ExestingName(domain.DomainName))
            {
                TempData["Message"] = "✅ Already Added";
                TempData["MessageType"] = "danger";
                return View(domain);
            }
           


            if (ModelState.IsValid)
            {
                _unitofWork.DomainRepository.Save(domain);
                _unitofWork.Complete();

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
            _unitofWork.Complete();

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }


        public IActionResult Search(string name)
        {
            name = name?.Trim().ToLower() ?? "";

            var data=_unitofWork.DomainRepository.GetAll()
                .OrderByDescending(d=>d.Id)
                .Where(m=> !string.IsNullOrEmpty(m.DomainName) && m.DomainName.ToLower().Contains(name))
                .Select(m => new
                {
                    domainName=m.DomainName,
                    updateBranch=string.IsNullOrEmpty (m.UpdateBranch) ? "N/A" :m.UpdateBranch,
                    updateDate=m.LastUpdateDate.HasValue?m.LastUpdateDate.Value.ToString("dd-MM-yyyy") :"N/A",
                    domainType= m.DomainType == DomainEnum.Test
                                ? "Test"
        :                       m.DomainType == DomainEnum.Live
                                ? "Live"
                                : "Unknown",
                    status =m.Status==true?"Active":"Inactive"
                });

            return Json(data);
        }
        
    }
}
