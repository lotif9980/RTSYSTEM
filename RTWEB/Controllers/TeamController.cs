using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using System.Threading.Tasks;

namespace RTWEB.Controllers
{
    [Authorize]
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
            //if (_unitofWork.TeamRepository.ExestingTeam(team.Name))
            //{
            //    TempData["Message"] = "❌ Already Added";
            //    TempData["MessageType"] = "danger";
            //}

            var exesting=_unitofWork.TeamRepository.ExestingTeam(team.Name);
            if (exesting)
            {
                TempData["Message"] = "❌ Already Added";
                TempData["MessageType"] = "danger";
                return View(team);
            }

            if (ModelState.IsValid)
            {
                _unitofWork.TeamRepository.Save(team);
                var result = _unitofWork.Complete();

                if (result > 0)
                {
                    TempData["Message"] = "✅ Save Successful";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "❌ Save Failed";
                    TempData["MessageType"] = "danger";
                }

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
                TempData["MessageType"] = "success";
                return RedirectToAction("Index");
            }
            _unitofWork.TeamRepository.Delete(id);
            var result=_unitofWork.Complete();

            if(result > 0)
            {
                TempData["Message"] = "✅ Successfully Delete!";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Delete Failed";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }
    }
}
