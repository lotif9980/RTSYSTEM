using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IIssueRepository
    {
        public IEnumerable<IssueVM> GetIssues();
        public void Save(Issue issue);
        public void Delete(int id);

    }
}
