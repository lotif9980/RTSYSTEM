using RTWEB.Data;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public class ReportRepository:IReportRepository
    {
        protected readonly Db _db;

        public ReportRepository(Db db)
        {
            _db = db;
        }

        public IEnumerable<IssueVM> PendingIssueReport(int? projectId = null)
        {
           var data =(from issue in _db.Issues
                      where !projectId.HasValue || issue.ProjectId== projectId.Value
                      join pro in _db.Projects on issue.ProjectId equals pro.Id
                      where  issue.Status==Enum.IssueStatus.pending
                      select new IssueVM
                      {
                          Title=issue.Title,
                          ProjectName=pro.ProjectName,
                          Status=Enum.IssueStatus.pending
                      }).ToList();

            return data;
        }
    }
}
