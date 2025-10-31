using Microsoft.AspNetCore.Mvc;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IUserRepository
    {
        public IEnumerable <UserVM> GetAll();

    }
}
