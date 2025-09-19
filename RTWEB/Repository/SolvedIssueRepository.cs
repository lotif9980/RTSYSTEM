using RTWEB.Data;
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
    }
}
