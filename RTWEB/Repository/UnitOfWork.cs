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

        public UnitOfWork(Db db,IDomainRepository domainRepository,
                               IIssueRepository issueRepository,
                               ITeamRepository teamRepository,
                               IProjectRepository projectRepository,
                               IUpdateRepository updateRepository,
                               IReportRepository reportRepository,
                               IOurCustomerRepository ourCustomerRepository,
                               ICustomerIssueRepository customerIssueRepository,
                               ISolvedIssueRepository solvedIssueRepository,
                               ISolvedDetailRepository solvedDetailRepository,
                               IParentProjectsRepository parentProjectsRepository
                         )
        {
            this.db = db;
            DomainRepository =domainRepository;
            IssueRepository =issueRepository;
            TeamRepository =teamRepository;
            ProjectRepository =projectRepository;
            UpdateRepository =updateRepository;
            ReportRepository =reportRepository;
            OurCustomerRepository =ourCustomerRepository;
            CustomerIssueRepository =customerIssueRepository;
            SolvedIssueRepository=solvedIssueRepository;
            SolvedDetailRepository=solvedDetailRepository;
            ParentProjectsRepository =parentProjectsRepository;

        }
      
    }
}
