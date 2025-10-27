using Microsoft.EntityFrameworkCore;
using RTWEB.Data;
using RTWEB.Enum;
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

       
        public List<Team> GetTeams()
        {
            return _db.Teams.ToList();
        }

        public void Save(Team team)
        {
            _db.Teams.Add(team);
        }

        public void Delete(int id)
        {
            var data = _db.Teams.Find(id);
            _db.Teams.Remove(data);
        }

        public IEnumerable<Team> GetDeveleper()
        {
            var data = _db.Teams
                   .Where(p => p.Status ==TeamType.Engineer)
                   .ToList();

            return data;
        }

        public IEnumerable<Team> GetTester()
        {
            var data = _db.Teams
                   .Where(p => p.Status == TeamType.SQA)
                   .ToList();

            return data;
        }

        public Task<bool> IsUsedInAsync(int id)
        {
            return _db.Updates.AnyAsync(p=>p.TesterId == id || p.DeveloperId==id);
        }

        public bool ExestingTeam(string name, int? id = null)
        {
            if(id == null)
            {
                return _db.Teams.Any(x => x.Name == name);
            }
            else
            {
                return _db.Teams.Any(x => x.Name == name && x.Id !=id);
            }
           
        }

        public Team GetById(int id)
        {
            return _db.Teams.Find(id);
        }

        public void Update(Team team)
        {
           _db.Teams.Update(team);
        }
    }
}
