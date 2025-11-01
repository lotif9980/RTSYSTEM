using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IUserRepository
    {
        public IEnumerable <UserVM> GetAll();
        public bool ExestingCheck(string userName, int?id=null);
        public void Save(User user);
        public User GetFirstOrDefault(string userName,string password);
        public User GetById(int id);
        public void Update(User user);
        public void Delete(int id);
        public void ToggleStatusUpdate(int id);
    }
}
