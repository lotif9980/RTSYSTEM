using RTWEB.Data;

namespace RTWEB.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        protected readonly Db db;

    

        public IDomainRepository DomainRepository { get; set; }
        public IIssueRepository IssueRepository { get; set; }
        public ITeamRepository TeamRepository {  get; set; }   
        public IProjectRepository ProjectRepository {  get; set; }



        public UnitOfWork(Db db)
        {
            this.db = db;
            DomainRepository = new DomainRepository(db);
            IssueRepository= new IssueRepository(db);
            TeamRepository= new TeamRepository(db);
            ProjectRepository = new ProjectRepository(db);
        }
      
    }
}
