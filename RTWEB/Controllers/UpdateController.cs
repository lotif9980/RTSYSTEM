using Microsoft.AspNetCore.Mvc;
using RTWEB.Data;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;

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

        [HttpGet]
        public IActionResult Save()
        {
            var data = new UpdateSaveVM
            {
                Update=new Update(),
                Tester=_unitofwork.TeamRepository.GetTester() ,
                Developer=_unitofwork.TeamRepository.GetDeveleper(),
                Domain=_unitofwork.DomainRepository.GetAll()
            };
            var project = _unitofwork.ProjectRepository.GetProjects().ToList();
            ViewBag.Project = project;

            return View(data);
        }

        public IActionResult GetProjectByIssue(int projectId)
        {
            var project = _unitofwork.IssueRepository.GetAll()
                .Where(p => p.ProjectId == projectId).Select(b=> new
                {
                    b.Id,
                    b.Title
                });

            return Json(project);
        }

        [HttpPost]
        public IActionResult Save(UpdateSaveVM model) 
        {
            var update = new Update
            {
                DomainId=model.Update.DomainId,
                UpdateDate = model.Update.UpdateDate ?? DateTime.Now,
                BranchName =model.Update.BranchName,
                DeveloperId=model.Update.DeveloperId,
                TesterId=model.Update.TesterId,
                Status=model.Update.Status
            };

            if(model.Update.UpdateDetails !=null && model.Update.UpdateDetails.Count > 0)
            {
                update.UpdateDetails = model.Update.UpdateDetails
                   
                   .Select(d=> new UpdateDetail
                   {
                       IssueId=d.IssueId,

                   }).ToList();
            }

            _unitofwork.UpdateRepository.Save(update);

            TempData["Message"] = "✅ Save Successful";
            TempData["MessageType"] = "success";

            return RedirectToAction("Save");
        }

        public IActionResult Delete(int id)
        {

            return RedirectToAction("Index");
        }
    }
}
