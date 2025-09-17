using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IReportRepository
    {
        public IEnumerable<IssueVM> PendingIssueReport(int? projectId=null);
    }
}
