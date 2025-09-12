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

        public void Save(Domain domain)
        {
           _db.Add(domain);
           _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var data =_db.Domains.Find(id);
            _db.Domains.Remove(data);
            _db.SaveChanges();
        }
    }
}
