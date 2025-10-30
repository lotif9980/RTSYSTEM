using Microsoft.EntityFrameworkCore;
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
        }

        public void Delete(int id)
        {
            var data = _db.Projects.Find(id);
            _db.Projects.Remove(data);
        }

        public Task<bool> IsIssueUsedAsync(int id)
        {
           return _db.Issues.AnyAsync(x=>x.ProjectId == id);
        }

        public Project Find(int id)
        {
            return _db.Projects.FirstOrDefault(p=>p.Id==id);
        }

        public void Update(Project project)
        {
            
            var data =_db.Projects.FirstOrDefault(p=>p.Id==project.Id);
            if(data != null)
            {
                data.UpdateBranch = project.UpdateBranch;
                data.LastUpdateDate= project.LastUpdateDate;
            }
        }

        public bool ExestingProject(string name,int?id=null)
        {
            if (id == null)
            {
                return _db.Projects.Any(x => x.ProjectName == name);
            }
            else
            {
                return _db.Projects.Any(x => x.ProjectName == name && x.Id!=id);
            }
          
        }

        public void EditUpdate(Project project)
        {
            var data = _db.Projects.FirstOrDefault(p => p.Id == project.Id);
            if (data != null)
            {
                data.ProjectName = project.ProjectName;
                data.SourceName = project.SourceName;
            }
        }
    }
}
