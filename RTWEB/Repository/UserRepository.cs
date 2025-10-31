using RTWEB.Data;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly Db _db;
        
        public UserRepository(Db db)
        {
            _db = db;
        }

      

        public IEnumerable<UserVM> GetAll()
        {
            var data = (from u in _db.Users
                        join r in _db.Users on u.RoleId equals r.Id
                        select new UserVM
                        {
                            Id= u.Id,
                            Name= u.Name,
                            PhoneNo= u.PhoneNo,
                            Email= u.Email,
                            RoleName=r.Name,
                            UserName=u.UserName,
                            Status=u.Status
                        }).ToList();

            return data;
        }
        public bool ExestingCheck(string userName)
        {
            return _db.Users.Any(u => u.UserName == userName);
        }

        public void Save(User user)
        {
            _db.Add(user);
        }
    }
}
