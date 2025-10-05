using RTWEB.Enum;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface ICustomerIssueRepository
    {
        public IEnumerable<CustomerIssueVM> GetAll();
        public IEnumerable<CustomerIssueVM> GetSolved();
        public void Save(CustomerIssue vm);
        public void Delete(int id);
        public void UpdateStatus(int id, CustomerIssueStatus status);
        public Task<bool> IsUsedIssue(int id);
        public CustomerIssue GetByid(int id);
    }
}
