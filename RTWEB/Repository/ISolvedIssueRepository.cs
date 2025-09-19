using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface ISolvedIssueRepository
    {
        public IEnumerable<CustomerSolvedIssueVM> GetSolvedIssue();

        public void Save(SolvedIssue vm);
    }
}
