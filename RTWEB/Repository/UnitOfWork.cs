using RTWEB.Data;
using System.Diagnostics;

namespace RTWEB.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        protected readonly Db db;

    

        public IDomainRepository DomainRepository { get; set; }
        public IIssueRepository IssueRepository { get; set; }
        public ITeamRepository TeamRepository {  get; set; }   
        public IProjectRepository ProjectRepository {  get; set; }
        public IUpdateRepository UpdateRepository { get; set; }
        public IReportRepository ReportRepository { get; set; }
        public IOurCustomerRepository OurCustomerRepository { get; set; }
        public ICustomerIssueRepository CustomerIssueRepository {  get; set; }
        public ISolvedIssueRepository SolvedIssueRepository {  get; set; }
        public ISolvedDetailRepository SolvedDetailRepository {  get; set; }
        public IParentProjectsRepository ParentProjectsRepository { get; set; }

        public int Complete()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public UnitOfWork(Db db)
        {
            this.db = db;
            DomainRepository = new DomainRepository(db);
            IssueRepository= new IssueRepository(db);
            TeamRepository= new TeamRepository(db);
            ProjectRepository = new ProjectRepository(db);
            UpdateRepository = new UpdateRepository(db);
            ReportRepository = new ReportRepository(db);
            OurCustomerRepository = new OurCustomerRepository(db);
            CustomerIssueRepository = new CustomerIssueRepository(db);
            SolvedIssueRepository = new SolvedIssueRepository(db);
            SolvedDetailRepository = new SolvedDetailRepository(db);
            ParentProjectsRepository = new ParentProjectsRepository(db);

        }
      
    }
}
