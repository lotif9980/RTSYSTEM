using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IOurCustomerRepository
    {
        public IEnumerable<OurCustomerVM> GetAll();
        public IEnumerable<OurCustomer> CustomerList();
        public void Save(OurCustomer customer);

        public OurCustomer? GetLastCustomer();
        public string GenerateNewCode();
    }
}
