using RTWEB.Data;

namespace RTWEB.Repository
{
    public class CustomerIssueRepository :ICustomerIssueRepository
    {
        protected readonly Db _db;
        public CustomerIssueRepository(Db db)
        {
            _db = db;
        }


    }
}
