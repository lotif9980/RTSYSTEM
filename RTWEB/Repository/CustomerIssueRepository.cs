using RTWEB.Data;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public class CustomerIssueRepository :ICustomerIssueRepository
    {
        protected readonly Db _db;
        public CustomerIssueRepository(Db db)
        {
            _db = db;
        }

        public IEnumerable<CustomerIssueVM> GetAll()
        {
            var data =(from cIssue in _db.CustomerIssues
                       join ourCos in _db.OurCustomers on cIssue.CustomerId equals ourCos.Id
                       join dom in _db.Domains on cIssue.DomainId equals dom.Id
                       select new CustomerIssueVM
                       {
                            Id= cIssue.Id,
                            Domainname=dom.DomainName,
                            CustomerName=ourCos.CustomerName,
                            //Status=cIssue.Status,
                            CreateDate=cIssue.CreateDate,
                            Problem=cIssue.Problem
                       }).ToList();

            return data;
        }

        public void Save(CustomerIssue vm)
        {
            _db.Add(vm);
        }
    }
}
