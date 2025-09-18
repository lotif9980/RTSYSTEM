namespace RTWEB.Repository
{
    public interface IUnitofWork : IDisposable
    {
        IDomainRepository DomainRepository { get; }
        IIssueRepository IssueRepository { get; }
        ITeamRepository TeamRepository { get; }
        IProjectRepository ProjectRepository { get; }
        IUpdateRepository UpdateRepository { get; }
        IReportRepository ReportRepository { get; }
        IOurCustomerRepository OurCustomerRepository { get; }
        ICustomerIssueRepository CustomerIssueRepository { get; }
        ISolvedIssueRepository SolvedIssueRepository { get; }
        ISolvedDetailRepository SolvedDetailRepository { get; }

        int Complete();
    }
}
