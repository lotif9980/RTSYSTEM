using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using System.Threading.Tasks;

namespace RTWEB.Controllers
{
    public class TeamController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public TeamController(IUnitofWork unitofWork)
        {
           _unitofWork = unitofWork;
        }


        public IActionResult Index(int page=1,int pageSize=10)
        {
            var data=_unitofWork.TeamRepository.GetTeams()
                .OrderByDescending(d=>d.Id)
                .AsQueryable()
                .ToPagedList(page, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Team team)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.TeamRepository.Save(team);

                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";

                return RedirectToAction("Save");
            }
            return View(team);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var IsUsed=await _unitofWork.TeamRepository.IsUsedInAsync(id);
            if (IsUsed)
            {
                TempData["Message"] = "✅ Team Used in Updates!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }
            _unitofWork.TeamRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }
    }
}
