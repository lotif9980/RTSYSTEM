using RTWEB.Data;
using RTWEB.Models;

namespace RTWEB.Repository
{
    public class ParentProjectsRepository : IParentProjectsRepository
    {
        protected readonly Db _db;

        public ParentProjectsRepository(Db db)
        {
            _db = db;
        }

        public List<ParentProject> GetAll()
        {
          return  _db.ParentProjects.ToList();
        }
    }
}
