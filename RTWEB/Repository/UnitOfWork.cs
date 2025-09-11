using RTWEB.Data;

namespace RTWEB.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        protected readonly Db db;

    

        public IDomainRepository DomainRepository { get; set; }

        public UnitOfWork(Db db)
        {
            this.db = db;
            DomainRepository = new DomainRepository(db);
        }
      
    }
}
