using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IUpdateRepository
    {
        public IEnumerable<UpdateVM> GetUpdates();
        public void Save(Update update);
        public void Delete(int id);

    }
}
