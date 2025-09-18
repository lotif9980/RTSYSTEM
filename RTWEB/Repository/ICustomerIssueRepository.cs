using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface ICustomerIssueRepository
    {
        public IEnumerable<CustomerIssueVM> GetAll();
    }
}
