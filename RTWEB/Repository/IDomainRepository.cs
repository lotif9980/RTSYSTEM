using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface IDomainRepository
    {
        public List<Domain> GetAll();
        public void Save(Domain domain);
        public void Delete(int id);
        public Task<bool> IsDomainUseAsync(int id);

    }
}
