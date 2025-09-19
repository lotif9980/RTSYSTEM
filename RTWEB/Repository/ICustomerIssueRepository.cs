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
    }
}
