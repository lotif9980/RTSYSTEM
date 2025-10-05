using Microsoft.EntityFrameworkCore;
using RTWEB.Data;
using RTWEB.Enum;
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
                       where cIssue.Status==Enum.CustomerIssueStatus.pending
                       join ourCos in _db.OurCustomers on cIssue.CustomerId equals ourCos.Id
                       join dom in _db.Domains on cIssue.DomainId equals dom.Id
                       select new CustomerIssueVM
                       {
                           CustomerId=cIssue.CustomerId,
                            Id= cIssue.Id,
                            Domainname=dom.DomainName,
                            CustomerName=ourCos.CustomerName,
                           Status =Enum.CustomerIssueStatus.pending,
                           CreateDate =cIssue.CreateDate,
                            Problem=cIssue.Problem
                       }).ToList();

            return data;
        }

        public IEnumerable<CustomerIssueVM> GetSolved()
        {
            var data = (from cIssue in _db.CustomerIssues
                        where cIssue.Status == Enum.CustomerIssueStatus.solved
                        join ourCos in _db.OurCustomers on cIssue.CustomerId equals ourCos.Id
                        join dom in _db.Domains on cIssue.DomainId equals dom.Id
                        select new CustomerIssueVM
                        {
                            Id = cIssue.Id,
                            Domainname = dom.DomainName,
                            CustomerName = ourCos.CustomerName,
                            Status = Enum.CustomerIssueStatus.solved,
                            CreateDate = cIssue.CreateDate,
                            Problem = cIssue.Problem
                        }).ToList();

            return data;
        }

        public void Save(CustomerIssue vm)
        {
            _db.Add(vm);
        }

        public void Delete(int id)
        {
           var data=_db.CustomerIssues.Find(id);
            _db.Remove(data);
        }

        public void UpdateStatus(int id, CustomerIssueStatus status)
        {
            var data = _db.CustomerIssues.Find(id);

            if (data != null)
            {
                data.Status = status;
                _db.CustomerIssues.Update(data);
            }
        }

        public Task<bool> IsUsedIssue(int id)
        {
           return _db.SolvedDetails.AnyAsync(x=>x.IssueId == id);
        }

        public CustomerIssue GetByid(int id)
        {
            return _db.CustomerIssues.FirstOrDefault(x => x.Id == id);
        }
    }
}
