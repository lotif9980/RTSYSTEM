using RTWEB.Models;

namespace RTWEB.ViewModel
{
    public class UserSaveVM
    {
        public User User { get; set; }
        public IEnumerable<Role> Role { get; set; }
        public IEnumerable<Team> Team { get; set; }
    }
}
