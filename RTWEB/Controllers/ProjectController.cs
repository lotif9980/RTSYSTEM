using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RTWEB.Models;
using RTWEB.Repository;

namespace RTWEB.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public ProjectController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }

        public IActionResult Index(int page=1 , int pageSize=10)
        {
            var data = _unitofWork.ProjectRepository.GetProjects()
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
        public IActionResult Save(Project project)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.ProjectRepository.Save(project);

                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";

                return RedirectToAction("Save");
            }

            return View(project);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var isUsed=await _unitofWork.ProjectRepository.IsIssueUsedAsync(id);
            if (isUsed)
            {
                TempData["Message"] = "✅ Porject Used in Issue!";
                TempData["MessageType"] = "danger";

                return RedirectToAction("Index");
            }
            _unitofWork.ProjectRepository.Delete(id);

            TempData["Message"] = "✅ Successfully Delete!";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

        public IActionResult Search(string name)
        {
            name=name?.Trim().ToLower() ?? "";

            var data = _unitofWork.ProjectRepository.GetProjects()
                        .OrderByDescending(p=>p.Id)
                       .Where(m => !string.IsNullOrEmpty(m.ProjectName) && m.ProjectName.ToLower().Contains(name))
                       .Select(m=> new
                       {
                          id= m.Id,
                          projectName= m.ProjectName,
                          updateBrnach= string.IsNullOrEmpty(m.UpdateBranch) ? "N/A" : m.UpdateBranch,
                          updateDate = m.LastUpdateDate.HasValue
                                ? m.LastUpdateDate.Value.ToString("dd-MM-yyyy")
                                : "N/A"
                       }).ToList();

            return Json(data);
        }

    }
}
