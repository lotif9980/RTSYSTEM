using RTWEB.Data;
using RTWEB.Models;

namespace RTWEB.Repository
{
    public class TeamRepository : ITeamRepository
    {
        protected readonly Db _db;
        public TeamRepository(Db db)
        {
            _db = db;
        }

       
        public IEnumerable<Team> GetTeams()
        {
            return _db.Teams;
        }

        public void Save(Team team)
        {
            _db.Teams.Add(team);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _db.Teams.Find(id);
            _db.Teams.Remove(data);
            _db.SaveChanges();
        }


    }
}
