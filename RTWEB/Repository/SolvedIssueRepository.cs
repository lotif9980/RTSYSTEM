using RTWEB.Data;

namespace RTWEB.Repository
{
    public class SolvedIssueRepository : ISolvedIssueRepository
    {
        protected readonly Db _db;
        
        public SolvedIssueRepository(Db db)
        { 
            _db = db; 
        }
    }
}
