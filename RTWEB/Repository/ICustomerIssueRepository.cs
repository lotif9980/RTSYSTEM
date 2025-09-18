using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface ICustomerIssueRepository
    {
        public IEnumerable<CustomerIssueVM> GetAll();
        public void Save(CustomerIssue vm);
    }
}
