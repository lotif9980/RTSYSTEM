using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IIssueRepository
    {
        public IEnumerable<IssueVM> GetIssues();

    }
}
