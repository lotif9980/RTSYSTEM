using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IOurCustomerRepository
    {
        public IEnumerable<OurCustomerVM> GetAll();
    }
}
