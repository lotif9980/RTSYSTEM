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

        public void Save(Project project)
        {
           _db.Projects.Add(project);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _db.Projects.Find(id);
            _db.Projects.Remove(data);
            _db.SaveChanges();
        }

    }
}
