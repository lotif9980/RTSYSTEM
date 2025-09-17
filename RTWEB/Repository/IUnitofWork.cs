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

        int Complete();
    }
}
