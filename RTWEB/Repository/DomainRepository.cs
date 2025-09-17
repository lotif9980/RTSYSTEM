using Microsoft.EntityFrameworkCore;
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
        }

        public void Delete(int id)
        {
            var data =_db.Domains.Find(id);
            _db.Domains.Remove(data);
           
        }

        public Task<bool> IsDomainUseAsync(int id)
        {
            return _db.Updates.AnyAsync(x=>x.DomainId== id);
        }

        public void Update(Domain domain)
            {
            var data =_db.Domains.FirstOrDefault(p=>p.Id== domain.Id);

            if (data != null)
            {
                data.UpdateBranch = domain.UpdateBranch;
                data.LastUpdateDate= domain.LastUpdateDate;
            }
        }

        public Domain Find(int id)
        {
            return _db.Domains.FirstOrDefault(p=>p.Id==id);
        }
    }
}
