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
        public bool ExestingCheck(string userName , int? id=null)
        {
            if (id == null)
            {
                return _db.Users.Any(u => u.UserName == userName);
            }
            else
            {
                return _db.Users.Any(u=>u.UserName==userName && u.Id !=id);
            }
            
        }

        public void Save(User user)
        {
            _db.Add(user);
        }

        public User GetFirstOrDefault(string userName, string password)
        {
            return _db.Users.FirstOrDefault(x=>x.UserName==userName && x.Password==password);
        }

        public User GetById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Update(User user)
        {
            var data = _db.Users.Find(user.Id);
            if(data != null)
            {
                data.Name=user.Name;
                data.PhoneNo=user.PhoneNo;
                data.Email = user.Email;
                data.RoleId=user.RoleId;
                data.UserName = user.UserName;
                data.Password = user.Password;
                data.EmployeeId = user.EmployeeId;
            }
            
        }

        public void Delete(int id)
        {
            var data =_db.Users.Find(id);
            _db.Users.Remove(data);
        }

        public void ToggleStatusUpdate(int id)
        {
            var user=_db.Users.Find(id);
            if(user != null)
            {
                user.Status= !user.Status;
            }
        }
    }
}
