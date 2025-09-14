using Microsoft.EntityFrameworkCore.Diagnostics;
using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface IProjectRepository
    {
        public IEnumerable<Project> GetProjects();
        public void Save(Project project);
        public void Delete(int id);
        public Task<bool> IsIssueUsedAsync(int id);

    }
}
