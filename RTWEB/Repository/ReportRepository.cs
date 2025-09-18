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


        public IEnumerable<DomainReportVM> DomainWiseUpdate(int? domainId = null)
        {
            var data = (from up in _db.Updates
                        where !domainId.HasValue || up.DomainId==domainId.Value
                        join d in _db.Domains on up.DomainId equals d.Id
                        join dev in _db.Teams on up.DeveloperId equals dev.Id
                        join tes in _db.Teams on up.TesterId equals tes.Id
                        select new DomainReportVM
                        {
                         Id=up.Id,
                         BranchName=up.BranchName,
                         TesterName=tes.Name,
                         DeveloperName=dev.Name,
                         DomainName=d.DomainName,
                         DateTime=up.UpdateDate
                        }).OrderByDescending(x=>x.Id).ToList();

            return data;
        }

        public IEnumerable<DomainReportVM> DomainUpdateList(int? domainId = null)
        {
            var data = (from dom in _db.Domains
                        where !domainId.HasValue || dom.Id == domainId.Value
                        select new DomainReportVM
                        {
                            DomainName=dom.DomainName,
                            BranchName=dom.UpdateBranch,
                            DateTime=dom.LastUpdateDate
                        }).ToList();
            return data;
        }
    }
}
