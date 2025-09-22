using Microsoft.EntityFrameworkCore;
using RTWEB.Data;
using RTWEB.Enum;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public class SolvedIssueRepository : ISolvedIssueRepository
    {
        protected readonly Db _db;
        
        public SolvedIssueRepository(Db db)
        { 
            _db = db; 
        }

      

        public IEnumerable<CustomerSolvedIssueVM> GetSolvedIssue()
        {
            var data=(from s in _db.SolvedIssues
                      join t in _db.Teams on s.SolvedBy equals t.Id
                      join oc in _db.OurCustomers on s.CustomerId equals oc.Id
                      select new CustomerSolvedIssueVM
                      {
                          Id=s.Id,
                          CustomerName=oc.CustomerName,
                          SolvedBy=t.Name,
                          SolvedDate=s.SolvedDate,
                          Status=s.Status
                      }).ToList();

            return data;
        }

        public void Save(SolvedIssue vm)
        {
           _db.Add(vm);
        }

        public IEnumerable<SolvedDetail> GetById(int id)
        {
            return _db.SolvedDetails.Where(p => p.SolvedIssueId == id).ToList();
        }

        public void Delete(int id)
        {
            var data =_db.SolvedIssues.Include(p=>p.SolvedDetails).FirstOrDefault(p=>p.Id==id);

            _db.SolvedDetails.RemoveRange(data.SolvedDetails);
            _db.SolvedIssues.Remove(data);
        }

        public CustomerSolvedIssueVM Details(int id)
        {
            var data = (from cIssue in _db.SolvedIssues
                        where cIssue.Id==id
                        join oc in _db.OurCustomers on cIssue.CustomerId equals oc.Id
                        join dom in _db.Domains on cIssue.DomainId equals dom.Id
                        join team in _db.Teams on cIssue.SolvedBy equals team.Id
                        select new CustomerSolvedIssueVM
                        {
                            CustomerName=oc.CustomerName,
                            SolvedBy=team.Name,
                            SolvedDate=cIssue.SolvedDate,
                            DomainName=dom.DomainName

                        }).FirstOrDefault();

            data.SolveDetails= (from SD in _db.SolvedDetails
                                join ci in _db.CustomerIssues on SD.IssueId equals ci.Id
                                where SD.SolvedIssueId == id
                                select new SolveDetails
                                {
                                    IssueName=ci.Problem,
                                    SolvedIssueId = SD.SolvedIssueId,
                                }).ToList();

            return data;

        }
    }
}
