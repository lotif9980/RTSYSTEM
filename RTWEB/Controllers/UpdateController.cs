using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Data;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;

namespace RTWEB.Controllers
{
    [Authorize]
    public class UpdateController : Controller
    {
        protected readonly IUnitofWork _unitofwork;
        public UpdateController(IUnitofWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }


        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data = _unitofwork.UpdateRepository.GetUpdates()
                .OrderByDescending(d=>d.Id)
                .AsQueryable()
                .ToPagedList(page, pageSize);
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
            //var domainAId = model.Update.DomainId;
            var projectId = model.ProjectId;
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

            var project = _unitofwork.ProjectRepository.Find(projectId);
            if (project != null)
            {
                project.UpdateBranch = model.Update.BranchName;
                project.LastUpdateDate = model.Update.UpdateDate;

                _unitofwork.ProjectRepository.Update(project);
            }


            var domain = _unitofwork.DomainRepository.Find(model.Update.DomainId);
            if(domain != null)
            {
                domain.UpdateBranch = model.Update.BranchName;
                domain.LastUpdateDate= model.Update.UpdateDate;

                _unitofwork.DomainRepository.Update(domain);
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
