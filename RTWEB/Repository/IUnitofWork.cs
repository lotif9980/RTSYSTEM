namespace RTWEB.Repository
{
    public interface IUnitofWork
    {
        IDomainRepository DomainRepository { get; }
    }
}
