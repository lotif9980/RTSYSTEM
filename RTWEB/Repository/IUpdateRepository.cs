using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IUpdateRepository
    {
        public IEnumerable<UpdateVM> GetUpdates();
        public void Save(Update update);
        public void Delete(int id);
        public UpdateVM GetDetails(int id);
        public IEnumerable<UpdateDetail> GetbyUpdateId(int id);
    }
}
