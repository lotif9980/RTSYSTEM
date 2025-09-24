using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IReportRepository
    {
        public IEnumerable<IssueVM> PendingIssueReport(int? projectId=null);
        public IEnumerable<DomainReportVM> DomainWiseUpdate(int? domainId=null);
        public IEnumerable<DomainReportVM> DomainUpdateList(int? domainId=null);
        public IEnumerable<DomainReportVM> ProjectWiseDomain(int? projectId=null);
        public List<CustomerSolvedIssueVM> CustomerSolvedIssue(int? domainId = null, int?customerId=null);
        public List<CustomerSolvedIssueVM> CustomerDailySupport(DateTime? fromDate, DateTime? toDate, int? domainId = null, int?customerId=null, int? solvedBy = null);

        public List<CustomerIssueVM> CustomerLedger( int customerId);
        public List<CustomerIssueVM> PendingSupport(int? domainId = null, int? customerId = null);
    }
}
