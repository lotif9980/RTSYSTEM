using Microsoft.EntityFrameworkCore;
using RTWEB.Data;
using RTWEB.Enum;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public class IssueRepository : IIssueRepository
    {
        protected readonly Db _db;
        public IssueRepository(Db db)
        {
            _db = db;
        }

        
        public IEnumerable<IssueVM> GetIssues()
        {
            var data=(from issue in _db.Issues
                      join project in _db.Projects on issue.ProjectId equals project.Id
                      where issue.Status==Enum.IssueStatus.pending
                      select new IssueVM
                      {
                          Id= issue.Id,
                          Title= issue.Title,
                          Description= issue.Description,
                          ProjectName=project.ProjectName,
                          Status=Enum.IssueStatus.pending
                      }).ToList();

           return data;
        }

        public IEnumerable<IssueVM> GetSolved()
        {
            var data = (from issue in _db.Issues
                        join project in _db.Projects on issue.ProjectId equals project.Id
                        where issue.Status == Enum.IssueStatus.solved
                        select new IssueVM
                        {
                            Id = issue.Id,
                            Title = issue.Title,
                            Description = issue.Description,
                            ProjectName = project.ProjectName,
                            Status=Enum.IssueStatus.solved
                           
                        }).ToList();

            return data;
        }
        public void Save(Issue issue)
        {
            _db.Issues.Add(issue);
        }

        public void Delete(int id)
        {
            var data =_db.Issues.Find(id);
            _db.Issues.Remove(data);
        }

        public IEnumerable<Issue> GetAll()
        {
            return _db.Issues;
        }

        public Task<bool> IsUsedAsync(int id)
        {
            return _db.UpdateDetails.AnyAsync(x => x.IssueId == id);
        }

        public void UpdateStatus(int id, IssueStatus status)
        {
           var data= _db.Issues.Find(id);
            if(data  != null)
            {
                data.Status = status;
            }
        }

        public bool ExestingIssue(int projectId, string problem)
        {
            return _db.Issues.Any(x => x.ProjectId == projectId && x.Title == problem);
        }
    }
}
