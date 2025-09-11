using RTWEB.Data;
using RTWEB.Models;

namespace RTWEB.Repository
{
    public class DomainRepository : IDomainRepository
    {
        protected readonly Db _db;
        public DomainRepository(Db db)
        {
            this._db = db;
        }

        public List<Domain> GetAll()
        {
           return _db.Domains.ToList();
        }
    }
}
