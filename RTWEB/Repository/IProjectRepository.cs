using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface IProjectRepository
    {
        public IEnumerable<Project> GetProjects();
    }
}
