using RTWEB.Models;
using RTWEB.ViewModel;
using RTWEB.Enum;

namespace RTWEB.Repository
{
    public interface IIssueRepository
    {
        public IEnumerable<IssueVM> GetIssues();
        public IEnumerable<IssueVM> GetSolved();
        public void Save(Issue issue);
        public void Delete(int id);
        public IEnumerable<Issue> GetAll();
        public Task<bool> IsUsedAsync(int id);
        public void UpdateStatus(int id, IssueStatus status);

    }
}
