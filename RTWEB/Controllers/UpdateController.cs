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
                .Where(p => p.ProjectId == projectId && p.Status==Enum.IssueStatus.pending).Select(b=> new
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

            if(model.UpdateDetails !=null && model.UpdateDetails.Count > 0)
            {
                update.UpdateDetails = model.UpdateDetails
                   
                   .Select(d=> new UpdateDetail
                   {
                       IssueId=d.IssueId,

                   }).ToList();
            }

            _unitofwork.UpdateRepository.Save(update);
             if(model.UpdateDetails !=null && model.UpdateDetails.Count > 0)
             {
                foreach(var detail in model.UpdateDetails)
                {
                    _unitofwork.IssueRepository.UpdateStatus(detail.IssueId, Enum.IssueStatus.solved);
                }
             }
           

            TempData["Message"] = "✅ Save Successful";
            TempData["MessageType"] = "success";

            return RedirectToAction("Save");
        }

        public IActionResult Delete(int id)
        {
          
            var updateDetails = _unitofwork.UpdateRepository.GetbyUpdateId(id);

            if(updateDetails !=null && updateDetails.Any())
            {
                foreach(var d in updateDetails)
                {
                    _unitofwork.IssueRepository.UpdateStatus(d.IssueId, Enum.IssueStatus.pending);
                }
            }

            _unitofwork.UpdateRepository.Delete(id);

            TempData["Message"] = "✅ Delete Successful";
            TempData["MessageType"] = "danger";


            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var data =_unitofwork.UpdateRepository.GetDetails(id);
            return View(data);
        }
    }
}
