using RTWEB.Data;
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
                      select new IssueVM
                      {
                          Id= issue.Id,
                          Title= issue.Title,
                          Description= issue.Description,
                          ProjectName=project.ProjectName
                      }).ToList();

           return data;
        }

        public void Save(Issue issue)
        {
            _db.Add(issue);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var data =_db.Issues.Find(id);
            _db.Issues.Remove(data);
            _db.SaveChanges();
        }

    }
}
