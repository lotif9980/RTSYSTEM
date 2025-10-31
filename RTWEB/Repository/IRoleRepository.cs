using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface IRoleRepository
    {
        public IEnumerable<Role> GetAll();
    }
}
