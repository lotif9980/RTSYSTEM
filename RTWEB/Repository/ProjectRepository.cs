using RTWEB.Data;
using RTWEB.Models;

namespace RTWEB.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        protected readonly Db _db;
        public ProjectRepository(Db db)
        {
            _db = db;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _db.Projects;
        }

    }
}
