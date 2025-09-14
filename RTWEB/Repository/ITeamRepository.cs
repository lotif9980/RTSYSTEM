using RTWEB.Models;

namespace RTWEB.Repository
{
    public interface ITeamRepository
    {
        public IEnumerable<Team> GetTeams();
        public void Save(Team team);
        public void Delete(int id);
        public IEnumerable<Team> GetDeveleper();
        public IEnumerable<Team> GetTester();
        public Task<bool> IsUsedInAsync(int id);
    }
}
