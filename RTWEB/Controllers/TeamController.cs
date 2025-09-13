using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    public class TeamController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public TeamController(IUnitofWork unitofWork)
        {
           _unitofWork = unitofWork;
        }


        public IActionResult Index()
        {
            var data=_unitofWork.TeamRepository.GetTeams();
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

                return RedirectToAction("Index");
            }
            return View(team);
        }

        public IActionResult Delete(int id)
        {
            _unitofWork.TeamRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }
    }
}
