using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface IParentProjectsRepository
    {
        public List<ParentProject> GetAll();
    }
}
