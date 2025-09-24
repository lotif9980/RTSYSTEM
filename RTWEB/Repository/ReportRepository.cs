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

        public List<CustomerSolvedIssueVM> CustomerSolvedIssue(int? domainId = null, int? customerId = null)
        {
            var query = from ci in _db.SolvedIssues
                        where (!customerId.HasValue || ci.CustomerId == customerId.Value)
                               && (!domainId.HasValue || ci.DomainId == domainId.Value)
                              && ci.Status == Enum.CustomerSolvedIssueStatus.Solved
                        join c in _db.OurCustomers on ci.CustomerId equals c.Id
                        join d in _db.Domains on ci.DomainId equals d.Id
                        join t in _db.Teams on ci.SolvedBy equals t.Id
                        select new CustomerSolvedIssueVM
                        {
                            Id = ci.Id,
                            CustomerName = c.CustomerName,
                            DomainName = d.DomainName,
                            SolvedBy = t.Name,
                            SolvedDate = ci.SolvedDate
                        };

            var dataList = query.ToList();

            // solve details attach
            foreach (var item in dataList)
            {
                item.SolveDetails = (from sd in _db.SolvedDetails
                                     join cissu in _db.CustomerIssues on sd.IssueId equals cissu.Id
                                     where sd.SolvedIssueId == item.Id
                                     select new SolveDetails
                                     {
                                         IssueName = cissu.Problem
                                     }).ToList();
            }

            return dataList
                .OrderBy(x => x.SolvedDate)
                .ToList();
        }

        

        public List<CustomerSolvedIssueVM> CustomerDailySupport(DateTime? fromDate, DateTime? toDate, int? domainId = null, int? customerId = null , int? solvedBy=null)
        {
            var query = from ci in _db.SolvedIssues
                        where (!customerId.HasValue || ci.CustomerId == customerId.Value)
                               && (!domainId.HasValue || ci.DomainId == domainId.Value)
                               && (!solvedBy.HasValue || ci.SolvedBy == solvedBy.Value)
                               && ci.SolvedDate >= fromDate && ci.SolvedDate <= toDate
                              && ci.Status == Enum.CustomerSolvedIssueStatus.Solved 
                              
                        join c in _db.OurCustomers on ci.CustomerId equals c.Id
                        join d in _db.Domains on ci.DomainId equals d.Id
                        join t in _db.Teams on ci.SolvedBy equals t.Id
                        select new CustomerSolvedIssueVM
                        {
                            Id = ci.Id,
                            CustomerName = c.CustomerName,
                            DomainName = d.DomainName,
                            SolvedBy = t.Name,
                            SolvedDate = ci.SolvedDate
                        };

            var dataList = query.ToList();

            // solve details attach
            foreach (var item in dataList)
            {
                item.SolveDetails = (from sd in _db.SolvedDetails
                                     join cissu in _db.CustomerIssues on sd.IssueId equals cissu.Id
                                     where sd.SolvedIssueId == item.Id
                                     select new SolveDetails
                                     {
                                         IssueName = cissu.Problem
                                     }).ToList();
            }

            return dataList
                .OrderBy(x => x.SolvedDate)
                .ToList();
        }

        public List<CustomerIssueVM> CustomerLedger( int customerId)
        {
            var data = (from ci in _db.CustomerIssues
                        where ci.CustomerId == customerId
                        

                        join c in _db.OurCustomers on ci.CustomerId equals c.Id
                        join d in _db.Domains on ci.DomainId equals d.Id

                        join sd in _db.SolvedDetails on ci.Id equals sd.IssueId into sdGroup
                        from sd in sdGroup.DefaultIfEmpty()

                        join si in _db.SolvedIssues on sd.SolvedIssueId equals si.Id into siGroup
                        from si in siGroup.DefaultIfEmpty()
                        select new CustomerIssueVM
                        {
                            Id = ci.Id,
                            Problem = ci.Problem,
                            Domainname = d.DomainName,
                            CustomerName = c.CustomerName,
                            CreateDate = ci.CreateDate,
                            UpdateDate=si.SolvedDate,
                            Status = ci.Status
                        }).ToList();

            return data;
        }

        public List<CustomerIssueVM> PendingSupport(int? domainId = null, int? customerId = null)
        {
            var data = (from ci in _db.CustomerIssues
                        where (!customerId.HasValue || ci.CustomerId == customerId.Value)
                               && (!domainId.HasValue || ci.DomainId == domainId.Value)
                               && ci.Status==Enum.CustomerIssueStatus.pending
                        join c in _db.OurCustomers on ci.CustomerId equals c.Id
                        join d in _db.Domains on ci.DomainId equals d.Id

                        select new CustomerIssueVM
                        {
                            Id = ci.Id,
                            Problem = ci.Problem,
                            Domainname = d.DomainName,
                            CustomerName = c.CustomerName,
                            CreateDate = ci.CreateDate,
                            Status = ci.Status
                        }).ToList();

            return data;
        }
    }
}
