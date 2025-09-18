using RTWEB.Data;

namespace RTWEB.Repository
{
    public class SolvedDetailRepository : ISolvedDetailRepository
    {
        protected readonly Db _db;
        public SolvedDetailRepository(Db db)
        {
            _db = db;
        }


    }
}
