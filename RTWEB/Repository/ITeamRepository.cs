using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface ITeamRepository
    {
        public IEnumerable<Team> GetTeams();
        public void Save(Team team);
        public void Delete(int id);
    }
}
