using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface IDomainRepository
    {
        public List<Domain> GetAll();
    }
}
