using RTWEB.Enum;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface ISolvedIssueRepository
    {
        public IEnumerable<CustomerSolvedIssueVM> GetSolvedIssue(DateTime?date=null);
        public void Save(SolvedIssue vm);
        public IEnumerable<SolvedDetail> GetById(int id); 
        public void Delete(int id); 
        public CustomerSolvedIssueVM Details(int id);
    }
}
