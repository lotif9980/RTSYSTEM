using RTWEB.Models;

namespace RTWEB.ViewModel
{
    public class CustomerIssueSaveVM
    {

        public CustomerIssue Input { get; set; } = new CustomerIssue();
        public List <CustomerIssue> CustomerIssue { get; set; }=new List<CustomerIssue>();
        public IEnumerable<Domain> Domain { get; set; }
        public IEnumerable<OurCustomerVM> OurCustomer { get; set; }
    }
}
