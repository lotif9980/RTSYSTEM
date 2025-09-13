using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IUpdateRepository
    {
        public IEnumerable<UpdateVM> GetUpdates();
    }
}
