using RTWEB.Data;
using RTWEB.Models;

namespace RTWEB.Repository
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly Db _db;

        public RoleRepository(Db db)
        {
          _db = db;
        }

        public IEnumerable<Role> GetAll()
        {
            return _db.Roles;
        }
    }
}
