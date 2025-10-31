using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IUserRepository
    {
        public IEnumerable <UserVM> GetAll();
        public bool ExestingCheck(string userName);
        public void Save(User user);

    }
}
